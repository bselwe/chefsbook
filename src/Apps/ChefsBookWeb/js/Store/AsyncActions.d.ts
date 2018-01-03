import { BaseAction } from "redux-actions";

declare interface AsyncDispatch<S = any> {
    <A extends BaseAction>(action: A): Promise<{ action: A, value: S }>;
}