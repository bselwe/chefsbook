import * as React from "react";
import * as moment from "moment";

import { Card, CardHeader, CardTitle, CardText, TextField, Checkbox, RaisedButton, AutoComplete, Chip } from "material-ui";
import InputMask from "react-input-mask";

import ActionSettings from "material-ui/svg-icons/action/settings";
import ContentSave from "material-ui/svg-icons/content/save";
import ContentAdd from "material-ui/svg-icons/content/add";
import { ContentRemoveCircle } from "material-ui/svg-icons";
import { generate } from "uuidjs";
import { NumberTextField } from "../Infrastructure/NumberTextField";

type Ingredient = Models.UpdateRecipeIngredientDTO & { Id: string };
type Step = Models.UpdateRecipeStepDTO & { Id: string };
type Tag = Models.UpdateRecipeTagDTO & { Id: string };

interface RecipeEditorInfoState {
    readonly isPrivate: boolean;
    readonly title: string;
    readonly description: string;
    readonly image: string;
    readonly duration?: string;
    readonly servings?: number;
    readonly notes: string;
    readonly ingredients: Ingredient[];
    readonly steps: Step[];
    readonly tags: Tag[];
    readonly tagName: string;
}

interface RecipeEditorInfoProps {
    readonly recipe: Models.UpdateRecipeDTO;
    readonly tags: Models.TagDTO[];
    readonly saveRecipe: (recipe: Models.UpdateRecipeDTO) => void;
}

export class RecipeEditor extends React.Component<RecipeEditorInfoProps, RecipeEditorInfoState> {
    constructor(props: RecipeEditorInfoProps) {
        super(props);

        this.state = {
            isPrivate: props.recipe.IsPrivate,
            title: props.recipe.Title,
            description: props.recipe.Description,
            image: props.recipe.Image,
            duration: props.recipe.Duration,
            servings: props.recipe.Servings,
            notes: props.recipe.Notes,
            ingredients: props.recipe.Ingredients.map(ingredient => ({ Id: generate(), Quantity: ingredient.Quantity, Name: ingredient.Name })),
            steps: props.recipe.Steps.map(step => ({ Id: generate(), Duration: step.Duration, Description: step.Description })),
            tags: props.recipe.Tags.map(tag => ({ Id: generate(), Name: tag.Name })),
            tagName: ""
        };
    }

    private saveRecipe = () => {
        let recipe: Models.UpdateRecipeDTO = {
            IsPrivate: this.state.isPrivate,
            Title: this.state.title,
            Description: this.state.description,
            Image: this.state.image,
            Duration: this.state.duration && this.state.duration.length > 0 ? this.state.duration : undefined,
            Servings: this.state.servings && this.state.servings > 0 ? this.state.servings : undefined,
            Notes: this.state.notes,
            Ingredients: this.state.ingredients.map(ingredient => ({ Quantity: ingredient.Quantity, Name: ingredient.Name })),
            Steps: this.state.steps.map(step => ({ Duration: step.Duration, Description: step.Description })),
            Tags: this.state.tags.map(tag => ({ Name: tag.Name }))
        };

        console.warn(recipe);

        this.props.saveRecipe(recipe);
    }

    private addIngredient = () => {
        let ingredients = this.state.ingredients;
        ingredients.push({ Id: generate(), Quantity: "", Name: "" });
        this.setState({ ingredients });
    }

    private removeIngredient = (index: number) => {
        let ingredients = this.state.ingredients;
        ingredients.splice(index, 1);
        this.setState({ ingredients });
    }

    private setIngredient = (index: number, quantity: string, name: string) => {
        let ingredients = this.state.ingredients;
        let ingredient = ingredients[index];
        ingredient.Quantity = quantity;
        ingredient.Name = name;
        this.setState({ ingredients });
    }

    private addStep = () => {
        let steps = this.state.steps;
        steps.push({ Id: generate(), Duration: undefined, Description: "" });
        this.setState({ steps });
    }

    private removeStep = (index: number) => {
        let steps = this.state.steps;
        steps.splice(index, 1);
        this.setState({ steps });
    }

    private setStep = (index: number, description: string, duration?: string) => {
        let steps = this.state.steps;
        let step = steps[index];
        step.Duration = duration && duration.length > 0 ? duration : undefined;
        step.Description = description;
        this.setState({ steps });
    }

    private addTag = () => {
        let tag: Tag = { Id: generate(), Name: this.state.tagName };
        let tags = this.state.tags;
        tags.push(tag);
        this.setState({ tags: tags, tagName: "" });
    }

    private removeTag = (index: number) => {
        let tags = this.state.tags;
        tags.splice(index, 1);
        this.setState({ tags });
    }

    render() {
        return <div style={{ margin: "0 1rem" }}>
            <Checkbox
                label="Private recipe"
                labelPosition="right"
                checked={this.state.isPrivate}
                onCheck={() => this.setState({ isPrivate: !this.state.isPrivate })}
                style={{ margin: "1rem 0" }} />
            <TextField
                floatingLabelText="Title*"
                hintText="What's the name of your recipe?"
                style={{ display: "block" }}
                value={this.state.title}
                onChange={(_, title) => this.setState({ title })} />
            <TextField
                floatingLabelText="Description"
                hintText="Describe your dish shortly"
                style={{ display: "block" }}
                value={this.state.description}
                onChange={(_, description) => this.setState({ description })} />
            <TextField
                floatingLabelText="Image"
                hintText="Url address to recipe's image"
                style={{ display: "block" }}
                value={this.state.image}
                onChange={(_, image) => this.setState({ image })} />
            <TextField
                floatingLabelText="Duration"
                style={{ display: "block" }}
                onChange={(_, duration) => this.setState({ duration })}>
                <InputMask mask="99:99:99" maskChar=" " defaultValue={this.state.duration} />
            </TextField>
            <NumberTextField
                floatingLabelText="Servings"
                hintText="How many servings?"
                style={{ display: "block", marginBottom: "3rem" }}
                value={this.state.servings}
                onValueChange={(servings) => this.setState({ servings })} />

            <h3 style={{ marginTop: "4rem" }}>Tags</h3>
            <div style={{ marginBottom: "2.75rem" }}>
                <AutoComplete
                    hintText="Tag name"
                    openOnFocus={true}
                    filter={AutoComplete.fuzzyFilter}
                    dataSource={this.props.tags.map(tag => tag.Name)}
                    searchText={this.state.tagName}
                    onUpdateInput={tagName => this.setState({ tagName })} />

                <div style={{ display: "inline-block", marginLeft: "1rem" }}>
                    <ContentAdd style={{ cursor: "pointer", verticalAlign: "middle" }} onClick={this.addTag} />
                </div>

                {this.state.tags.length > 0 &&
                    <div style={{ display: "flex", flexWrap: "wrap", marginTop: "1rem" }}>
                        {this.state.tags.map((tag, i) =>
                            <Chip
                                key={tag.Id}
                                style={{ margin: "0.25rem" }}
                                onRequestDelete={() => this.removeTag(i)}>
                                {tag.Name}
                            </Chip>
                        )}
                    </div>
                }
            </div>

            <h3>Ingredients</h3>
            <table className="recipeEditorTable">
                <thead>
                    {this.state.ingredients.length > 0 && <tr>
                        <td>Quantity<br /><small>e.g. 2 tablespoons</small></td>
                        <td>Name<br /><small>i.e. eggs</small></td>
                        <td></td>
                    </tr>}
                </thead>
                <tbody>
                    {
                        this.state.ingredients.map((ingredient, i) => <tr key={ingredient.Id}>
                            <td style={{ verticalAlign: "bottom" }}>
                                <TextField
                                    id={`${ingredient.Id}-quantity`}
                                    value={ingredient.Quantity}
                                    inputStyle={{ display: "table-cell", verticalAlign: "bottom" }}
                                    onChange={(_, quantity) => this.setIngredient(i, quantity, ingredient.Name)} />
                            </td>
                            <td>
                                <TextField
                                    id={`${ingredient.Id}-name`}
                                    value={ingredient.Name}
                                    multiLine
                                    onChange={(_, name) => this.setIngredient(i, ingredient.Quantity, name)} />
                            </td>
                            <td>
                                <ContentRemoveCircle style={{ cursor: "pointer" }} onClick={() => this.removeIngredient(i)} />
                            </td>
                        </tr>)
                    }
                    <tr>
                        <td onClick={this.addIngredient} style={{ cursor: "pointer", paddingTop: "0.65rem" }}><ContentAdd style={{ verticalAlign: "middle" }} /> Ingredient</td>
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>

            <h3 style={{ marginTop: "3rem" }}>Steps</h3>
            <table className="recipeEditorTable">
                <thead>
                    {this.state.steps.length > 0 && <tr>
                        <td>Duration</td>
                        <td>Instructions</td>
                        <td></td>
                    </tr>}
                </thead>
                <tbody>
                    {
                        this.state.steps.map((step, i) => <tr key={step.Id}>
                            <td style={{ verticalAlign: "bottom" }}>
                                <TextField
                                    type="text"
                                    hintText="HH:MM:SS"
                                    onChange={(_, duration) => this.setStep(i, step.Description, duration)}>
                                    <InputMask mask="99:99:99" maskChar=" " defaultValue={step.Duration} />
                                </TextField>
                            </td>
                            <td>
                                <TextField
                                    id={`${step.Id}-description`}
                                    value={step.Description}
                                    multiLine
                                    onChange={(_, description) => this.setStep(i, description, step.Duration)} />
                            </td>
                            <td>
                                <ContentRemoveCircle style={{ cursor: "pointer" }} onClick={() => this.removeStep(i)} />
                            </td>
                        </tr>)
                    }
                    <tr>
                        <td onClick={this.addStep} style={{ cursor: "pointer", paddingTop: "0.65rem" }}><ContentAdd style={{ verticalAlign: "middle" }} /> Step</td>
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>

            <TextField
                floatingLabelText="Notes"
                hintText="Any notes?"
                style={{ display: "block", marginTop: "3rem" }}
                value={this.state.notes}
                multiLine
                onChange={(_, notes) => this.setState({ notes })} />

            <RaisedButton label="Save" primary style={{ marginTop: "3rem" }} onClick={this.saveRecipe} />
        </div>;
    }
}
