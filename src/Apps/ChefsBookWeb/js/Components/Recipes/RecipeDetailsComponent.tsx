import * as React from "react";
import * as Router from "react-router";
import * as routes from "../../Routes";
import { Card, CardText, CardMedia, CardTitle, Divider, Toolbar, ToolbarGroup, IconButton, ToolbarTitle, RaisedButton, FlatButton } from "material-ui";
import { RecipeDetailsStateProps, RecipeDetailsDispatchProps } from "./RecipeDetails";
import * as moment from "moment";
import { NavigationArrowBack, ActionShoppingBasket } from "material-ui/svg-icons";

type RecipeDetailsComponentProps = RecipeDetailsStateProps & RecipeDetailsDispatchProps;

export class RecipeDetailsComponent extends React.Component<RecipeDetailsComponentProps> {
    componentDidMount() {
        this.props.fetchUserInfo();
        this.props.fetchRecipe();
    }

    render() {
        let recipe = this.props.recipe;
        let servings = recipe && recipe.Servings ? `Yield: ${recipe.Servings} servings` : ``;
        let duration = recipe && recipe.Duration ? `Time: ${moment.duration(recipe.Duration).asMinutes()} mins` : ``;
        let separator = servings && duration ? `, ` : ``;

        return recipe ? <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text={`${servings}${separator}${duration}`} style={{ fontSize: "1rem" }} />
                </ToolbarGroup>
                <ToolbarGroup>
                    {this.props.user && recipe.UserId === this.props.user.Id && <div>
                        <RaisedButton label="Delete" secondary onClick={this.props.deleteRecipe} style={{ marginRight: "0.5rem" }} />
                        <RaisedButton label="Edit" primary onClick={this.props.navigateToEditRecipe} style={{ marginLeft: 0, marginRight: "1rem" }} />
                    </div>}
                    <IconButton tooltip={"Back"} onClick={this.props.goBack}>
                        <NavigationArrowBack />
                    </IconButton>
                </ToolbarGroup>
            </Toolbar>
            <CardMedia
                overlay={
                    <CardTitle title={recipe.Title} subtitle={recipe.Description}>
                    </CardTitle>}
                style={{ height: 400, background: `url('${recipe.Image}') no-repeat center center`, backgroundSize: "100% auto" }}>
            </CardMedia>

            <CardText className="clearfix" style={{ paddingBottom: "0.5rem" }}>
                <div style={{ float: "left" }}>
                    <FlatButton
                        label="Add to cart"
                        icon={<ActionShoppingBasket />}
                        onClick={() => this.props.addToCart(recipe.Id)} />
                </div>
                {recipe.Tags && recipe.Tags.length > 0 &&
                <div style={{ float: "right" }}>
                    {"Tags: " + recipe.Tags.map(tag => tag.Name).join(", ")}
                </div>}
            </CardText>

            <CardText style={{ display: "flex", margin: "0 1rem" }}>
                <div style={{ flex: "3 0", marginRight: "2rem" }}>
                    <CardTitle title="Ingredients" />
                    <Divider />
                    {recipe.Ingredients && <table style={{ marginTop: "1rem" }}>
                        <tbody>
                         {
                                recipe.Ingredients.map(ingredient =>
                                <tr key={ingredient.Name}>
                                    <td style={{ textAlign: "right", verticalAlign: "top", paddingRight: "1rem", paddingBottom: "0.5rem", whiteSpace: "nowrap" }}>
                                        {ingredient.Quantity}
                                    </td>
                                    <td style={{ verticalAlign: "top", paddingBottom: "0.5rem" }}>
                                        {ingredient.Name}
                                    </td>
                                </tr>)
                            }
                        </tbody>
                    </table>}
                </div>
                <div style={{ flex: "4 0" }}>
                    <div>
                        <CardTitle title="Steps" />
                        <Divider />
                        {recipe.Steps && <table style={{ marginTop: "1rem" }}>
                            <tbody>
                                {
                                    recipe.Steps.map(step =>
                                    <tr key={step.Description}>
                                        <td style={{ textAlign: "right", verticalAlign: "top", paddingRight: "1rem", paddingBottom: "0.5rem", whiteSpace: "nowrap" }}>
                                            {step.Duration}
                                        </td>
                                        <td style={{ verticalAlign: "top", paddingBottom: "0.5rem" }}>
                                            {step.Description}
                                        </td>
                                    </tr>)
                                }
                            </tbody>
                        </table>}
                    </div>
                    {recipe.Notes && recipe.Notes.length > 0 && <div>
                        <CardTitle title="Notes" />
                        <Divider style={{ marginBottom: "1rem" }}/>
                        {recipe.Notes}
                    </div>}
                </div>
            </CardText>
        </Card> : null;
    }
}
