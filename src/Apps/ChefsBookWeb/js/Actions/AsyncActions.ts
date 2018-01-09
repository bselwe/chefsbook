import { Action, createAction, ActionFunctionAny, ReducerMap, BaseAction, ActionFunction0, ActionFunction1, ActionFunction2, ActionFunction3 } from "redux-actions";

type F0<TPayload> = ActionFunction0<Action<Promise<TPayload>>>;
type F1<TPayload, TParam1> = ActionFunction1<TParam1, Action<Promise<TPayload>>>;
type F2<TPayload, TParam1, TParam2> = ActionFunction2<TParam1, TParam2, Action<Promise<TPayload>>>;
type F3<TPayload, TParam1, TParam2, TParam3> = ActionFunction3<TParam1, TParam2, TParam3, Action<Promise<TPayload>>>;
type FAny<TPayload> = ActionFunctionAny<Action<Promise<TPayload>>>;

export type RejectionParams = { message: string, stack: string };
export type ActionCreator<TState, TAction = BaseAction> = (state: TState, action: TAction) => TState;
export type OnStart<TState> = ActionCreator<TState>;
export type OnFulfilled<TState, TPayload> = ActionCreator<TState, Action<TPayload>>;
export type OnRejected<TState> = ActionCreator<TState, Action<RejectionParams>>;

type AsyncAction<TState, TPayload, TActionFunction> = { action: TActionFunction, reducers: ReducerMap<TState, any>; };

export function createAsyncAction<TState extends AsyncState, TPayload>(
    actionType: string,
    payloadCreator: () => Promise<TPayload>,
    onFulfilled?: OnFulfilled<TState, TPayload>,
    onStart?: OnStart<TState>,
    onRejected?: OnRejected<TState>): AsyncAction<TState, TPayload, F0<TPayload>>;

export function createAsyncAction<TState extends AsyncState, TPayload, TParam1>(
    actionType: string,
    payloadCreator: (param1: TParam1) => Promise<TPayload>,
    onFulfilled?: OnFulfilled<TState, TPayload>,
    onStart?: OnStart<TState>,
    onRejected?: OnRejected<TState>): AsyncAction<TState, TPayload, F1<TPayload, TParam1>>;

export function createAsyncAction<TState extends AsyncState, TPayload, TParam1, TParam2>(
    actionType: string,
    payloadCreator: (param1: TParam1, param2: TParam2) => Promise<TPayload>,
    onFulfilled?: OnFulfilled<TState, TPayload>,
    onStart?: OnStart<TState>,
    onRejected?: OnRejected<TState>): AsyncAction<TState, TPayload, F2<TPayload, TParam1, TParam2>>;

export function createAsyncAction<TState extends AsyncState, TPayload, TParam1, TParam2, TParam3>(
    actionType: string,
    payloadCreator: (param1: TParam1, param2: TParam2, param3: TParam3) => Promise<TPayload>,
    onFulfilled?: OnFulfilled<TState, TPayload>,
    onStart?: OnStart<TState>,
    onRejected?: OnRejected<TState>): AsyncAction<TState, TPayload, F3<TPayload, TParam1, TParam2, TParam3>>;

export function createAsyncAction<TState extends AsyncState, TPayload>(
    actionType: string,
    payloadCreator: (...args: any[]) => Promise<TPayload>,
    onFulfilled: OnFulfilled<TState, TPayload> = state => HandleFulfilled(state),
    onStart: OnStart<TState> = state => HandlePending(state),
    onRejected: OnRejected<TState> = state => HandleRejection(state)
): AsyncAction<TState, TPayload, FAny<TPayload>> {
    return {
        action: createAction(actionType, payloadCreator),
        reducers: {
            [actionType + "_PENDING"]: onStart,
            [actionType + "_FULFILLED"]: onFulfilled,
            [actionType + "_REJECTED"]: onRejected
        }
    };
}

function HandleFulfilled<TState extends AsyncState>(state: TState)  {
    return {
        ...state as any,
        ...EndTask(state)
    };
}

function HandlePending<TState extends AsyncState>(state: TState)  {
    return {
        ...state as any,
        ...StartTask(state)
    };
}

function HandleRejection<TState extends AsyncState>(state: TState)  {
    return {
        ...state as any,
        ...EndTask(state)
    };
}

export function StartTask(state: AsyncState): AsyncState {
    return { tasksCount: state.tasksCount + 1 };
}

export function EndTask(state: AsyncState): AsyncState {
    return { tasksCount: state.tasksCount > 0 ? state.tasksCount - 1 : 0 };
}

export const startTask = createAction("TASKS/START");
export const endTask = createAction("TASK/END");

export const asyncActionsReducers = {
    [startTask.toString()](state: AppState) { return { ...state, ...StartTask(state) }; },
    [endTask.toString()](state: AppState) { return { ...state, ...EndTask(state) }; }
};
