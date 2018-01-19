import * as React from "react";
import * as Router from "react-router";

import * as routes from "../../Routes";
import { MenuItem, AppBar, Drawer, MenuItemProps } from "material-ui";
import { connect } from "react-redux";
import { signOut } from "../../Actions/Account";
import { ActionExitToApp, MapsRestaurant, ActionShoppingBasket, MapsRestaurantMenu } from "material-ui/svg-icons";
import { RouteComponentProps } from "react-router";
import { UserProfile } from "../Account/UserProfile";

interface MenuDispatchProps {
    readonly signOut: () => void;
}

type MenuProps = MenuDispatchProps;

interface MenuState {
    readonly isOpen: boolean;
}

class MenuDisplay extends React.Component<MenuProps, MenuState> {
    constructor(props: MenuProps) {
        super(props);

        this.state = {
            isOpen: true
        };
    }

    private toggleMenu = () => {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    public render() {
        return <div>
            <Drawer open={this.state.isOpen}>
                <AppBar onLeftIconButtonClick={this.toggleMenu} />

                <MenuLink to={routes.MyRecipes} primaryText={"My recipes"} leftIcon={<MapsRestaurant />} />
                <MenuLink to={routes.PublicRecipes} primaryText={"Explore"} leftIcon={<MapsRestaurantMenu />} />
                <MenuLink to={routes.Cart} primaryText={"Cart"} leftIcon={<ActionShoppingBasket />} />
                <MenuItem onClick={this.props.signOut} insetChildren leftIcon={<ActionExitToApp />}>Sign out</MenuItem>
            </Drawer>

            <AppBar
                title={"ChefsBook"}
                style={{ zIndex: 100000 }}
                onLeftIconButtonClick={this.toggleMenu}>
                <UserProfile />
            </AppBar>

            <div style={{ paddingLeft: this.state.isOpen ? 256 : 0, width: "100%", boxSizing: "border-box" }}>
                <div style={{ padding: "2rem" }}>
                    {
                        ...this.props.children as any
                    }
                </div>
            </div>
        </div>;
    }
}

interface MenuLinkProps extends MenuItemProps, RouteComponentProps<any> {
    readonly to: string;
}

const MenuLink = Router.withRouter<MenuLinkProps>(props => {
    let { match, location, history, staticContext, ...menuItemProps } = props;
    return <MenuItem {...menuItemProps} insetChildren onClick={() => props.history.push(props.to)} style={{ whiteSpace: "normal" }} />;
});

export const Menu = connect(
    undefined,
    (dispatch): MenuDispatchProps => {
        return {
            signOut: () => dispatch(signOut())
        };
    }
)(MenuDisplay);
