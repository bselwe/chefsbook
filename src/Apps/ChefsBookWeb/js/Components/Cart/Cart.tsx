import * as React from "react";
import * as Router from "react-router";
import { Card, Toolbar, ToolbarTitle, ToolbarGroup, CardText, RaisedButton, Table, TableBody, TableRow, TableRowColumn, TableHeaderColumn } from "material-ui";
import { Dispatch, connect } from "react-redux";
import { fetchCart, deleteCart } from "../../Actions/Cart";

interface CartProps extends Router.RouteComponentProps<{}> {
}

interface CartStateProps {
    readonly cart: Models.IngredientDTO[];
}

interface CartDispatchProps {
    readonly fetchCart: () => void;
    readonly deleteCart: () => void;
}

type CartComponentProps = CartStateProps & CartDispatchProps;

export class CartComponent extends React.Component<CartComponentProps> {
    componentDidMount() {
        this.props.fetchCart();
    }

    render() {
        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text="Cart" />
                </ToolbarGroup>
                <ToolbarGroup>
                    <RaisedButton label="Clear cart" secondary onClick={this.props.deleteCart} style={{ margin: 0 }} />
                </ToolbarGroup>
            </Toolbar>

            <CardText>
                <Table selectable={false} style={{ overflow: "auto", tableLayout: "auto" }}>
                    <TableBody displayRowCheckbox={false}>
                        <TableRow>
                            <TableHeaderColumn>Quantity</TableHeaderColumn>
                            <TableHeaderColumn>Name</TableHeaderColumn>
                        </TableRow>
                        {
                            this.props.cart.map(ingredient => <TableRow>
                                <TableRowColumn>{ingredient.Quantity}</TableRowColumn>
                                <TableRowColumn>{ingredient.Name}</TableRowColumn>
                            </TableRow>)
                        }
                    </TableBody>
                </Table>
            </CardText>
        </Card>;
    }
}

const mapStateToProps = (state: CartState, props: CartProps): CartStateProps => {
    return {
        cart: state.cart
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: CartProps): CartDispatchProps => {
    return {
        fetchCart: () => dispatch(fetchCart()),
        deleteCart: async () => {
            await dispatch(deleteCart());
            await dispatch(fetchCart());
        }
    };
};

export const Cart = connect(
    mapStateToProps,
    mapDispatchToProps as any as CartDispatchProps
)(CartComponent);
