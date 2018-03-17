import * as React from "react";
import * as Router from "react-router";
import { connect, Dispatch } from "react-redux";

import { fetchUserInfo } from "../../Actions/Account";

interface UserProfileStateProps {
    readonly user?: Models.UserInfoDTO;
}

interface UserProfileDispatchProps {
    readonly fetchUserInfo: () => void;
}

type UserProfileProps = UserProfileStateProps & UserProfileDispatchProps;

class UserProfileComponent extends React.Component<UserProfileProps> {
    componentDidMount() {
        this.props.fetchUserInfo();
    }

    render() {
        let user = this.props.user;

        return user ? <div style={{ color: "white", fontSize: "15px", lineHeight: "64px" }}>
            <span>{`${user.FirstName} ${user.LastName} (${user.Email})`}</span>
            <img src={user.Photo} style={{ verticalAlign: "middle", width: "32px", height: "32px", borderRadius: "100px", marginLeft: "16px"}} />
        </div> : null;
    }
}

export const UserProfile = connect(
    (state: AccountState): UserProfileStateProps => {
        return {
            user: state.user,
        };
    },
    (dispatch: Dispatch<any>): UserProfileDispatchProps => {
        return {
            fetchUserInfo: () => dispatch(fetchUserInfo())
        };
    }
)(UserProfileComponent);
