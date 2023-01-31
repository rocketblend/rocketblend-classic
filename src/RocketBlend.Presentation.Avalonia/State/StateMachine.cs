using System;
using System.Collections.Generic;

namespace RocketBlend.Presentation.Avalonia.State;

/// <summary>
/// StateMachine - api based on: https://github.com/dotnet-state-machine/stateless
/// </summary>
public class StateMachine<TState, TTrigger> where TTrigger : Enum where TState : struct, Enum
{
    private StateContext _currentState;
    private readonly Dictionary<TState, StateContext> _states;
    private OnTransitionedDelegate? _onTransitioned;

    public delegate void OnTransitionedDelegate(TState from, TState to);

    public TState State => this._currentState.StateId;

    /// <summary>
    /// Are the in state.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <returns>A bool.</returns>
    public bool IsInState(TState state)
    {
        return this.IsAncestorOf(this._currentState.StateId, state);
    }

    public StateMachine(TState initialState)
    {
        this._states = new Dictionary<TState, StateContext>();

        this.RegisterStates();

        this._currentState = this.Configure(initialState);
    }

    /// <summary>
    /// Registers the states.
    /// </summary>
    private void RegisterStates()
    {
        foreach (var state in Enum.GetValues<TState>())
        {
            this._states.Add(state, new StateContext(this, state));
        }
    }

    /// <summary>
    /// Ons the transitioned.
    /// </summary>
    /// <param name="onTransitioned">The on transitioned.</param>
    /// <returns>A StateMachine.</returns>
    public StateMachine<TState, TTrigger> OnTransitioned(OnTransitionedDelegate onTransitioned)
    {
        this._onTransitioned = onTransitioned;

        return this;
    }

    /// <summary>
    /// Are the ancestor of.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="parent">The parent.</param>
    /// <returns>A bool.</returns>
    private bool IsAncestorOf(TState state, TState parent)
    {
        if (this._states.ContainsKey(state))
        {
            StateContext current = this._states[state];

            while (true)
            {
                if (current.StateId.Equals(parent))
                {
                    return true;
                }

                if (current.Parent is { })
                {
                    current = current.Parent;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Configures the.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <returns>A StateContext.</returns>
    public StateContext Configure(TState state)
    {
        return this._states[state];
    }

    /// <summary>
    /// Fires the.
    /// </summary>
    /// <param name="trigger">The trigger.</param>
    public void Fire(TTrigger trigger)
    {
        this._currentState.Process(trigger);

        if (this._currentState.CanTransit(trigger))
        {
            var destination = this._currentState.GetDestination(trigger);

            if (this._states.ContainsKey(destination) && this._states[destination].Parent is { } parent && !this.IsInState(parent.StateId))
            {
                this.Goto(parent.StateId);
            }

            this.Goto(destination);
        }
        else if (this._currentState.Parent is { } && this._currentState.Parent.CanTransit(trigger))
        {
            this.Goto(this._currentState.Parent.StateId, true, false);
            this.Goto(this._currentState.GetDestination(trigger));
        }
    }

    /// <summary>
    /// Starts the.
    /// </summary>
    public void Start()
    {
        this.Enter();
    }

    /// <summary>
    /// Enters the.
    /// </summary>
    private void Enter()
    {
        this._currentState.Enter();

        if (this._currentState.InitialTransitionTo is { } state)
        {
            this.Goto(state);
        }
    }

    /// <summary>
    /// Gotos the.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="exit">If true, exit.</param>
    /// <param name="enter">If true, enter.</param>
    private void Goto(TState state, bool exit = true, bool enter = true)
    {
        if (this._states.ContainsKey(state))
        {
            if (exit && !this.IsAncestorOf(state, this._currentState.StateId))
            {
                this._currentState.Exit();
            }

            var old = this._currentState.StateId;

            this._currentState = this._states[state];

            this._onTransitioned?.Invoke(old, this._currentState.StateId);

            if (enter)
            {
                this.Enter();
            }
        }
    }

    public class StateContext
    {
        private readonly Dictionary<TTrigger, TState> _permittedTransitions;
        private readonly StateMachine<TState, TTrigger> _owner;
        private readonly List<Action> _entryActions;
        private readonly List<Action> _exitActions;
        private readonly Dictionary<TTrigger, List<Action>> _triggerActions;

        public TState StateId { get; }

        public StateContext? Parent { get; private set; }

        internal TState? InitialTransitionTo { get; private set; }

        public StateContext(StateMachine<TState, TTrigger> owner, TState state)
        {
            this._owner = owner;
            this.StateId = state;

            this._entryActions = new();
            this._exitActions = new();
            this._triggerActions = new();
            this._permittedTransitions = new();
        }

        /// <summary>
        /// Initials the transition.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>A StateContext.</returns>
        public StateContext InitialTransition(TState? state)
        {
            this.InitialTransitionTo = state;

            return this;
        }

        /// <summary>
        /// Substates the of.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A StateContext.</returns>
        public StateContext SubstateOf(TState parent)
        {
            this.Parent = this._owner._states[parent];

            return this;
        }

        /// <summary>
        /// Permits the.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <param name="state">The state.</param>
        /// <returns>A StateContext.</returns>
        public StateContext Permit(TTrigger trigger, TState state)
        {
            if (this.StateId.Equals(state))
            {
                throw new InvalidOperationException("Configuring state re-entry is not allowed.");
            }

            this._permittedTransitions[trigger] = state;

            return this;
        }

        /// <summary>
        /// Ons the entry.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>A StateContext.</returns>
        public StateContext OnEntry(Action action)
        {
            this._entryActions.Add(action);

            return this;
        }

        /// <summary>
        /// Customs the.
        /// </summary>
        /// <param name="custom">The custom.</param>
        /// <returns>A StateContext.</returns>
        public StateContext Custom(Func<StateContext, StateContext> custom)
        {
            return custom(this);
        }

        /// <summary>
        /// Ons the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <param name="action">The action.</param>
        /// <returns>A StateContext.</returns>
        public StateContext OnTrigger(TTrigger trigger, Action action)
        {
            if (this._triggerActions.TryGetValue(trigger, out var t))
            {
                t.Add(action);
            }
            else
            {
                this._triggerActions.Add(trigger, new List<Action> { action });
            }

            return this;
        }

        /// <summary>
        /// Ons the exit.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>A StateContext.</returns>
        public StateContext OnExit(Action action)
        {
            this._exitActions.Add(action);

            return this;
        }

        /// <summary>
        /// Enters the.
        /// </summary>
        internal void Enter()
        {
            foreach (var action in this._entryActions)
            {
                action();
            }
        }

        /// <summary>
        /// Exits the.
        /// </summary>
        internal void Exit()
        {
            foreach (var action in this._exitActions)
            {
                action();
            }
        }

        /// <summary>
        /// Processes the.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        internal void Process(TTrigger trigger)
        {
            if (this._triggerActions.ContainsKey(trigger) && this._triggerActions[trigger] is { } actions)
            {
                foreach (var action in actions)
                {
                    action();
                }
            }

            if (this.Parent is { })
            {
                this.Parent.Process(trigger);
            }
        }

        /// <summary>
        /// Cans the transit.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns>A bool.</returns>
        internal bool CanTransit(TTrigger trigger)
        {
            return this._permittedTransitions.ContainsKey(trigger);
        }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns>A TState.</returns>
        internal TState GetDestination(TTrigger trigger)
        {
            return this._permittedTransitions[trigger];
        }
    }
}
