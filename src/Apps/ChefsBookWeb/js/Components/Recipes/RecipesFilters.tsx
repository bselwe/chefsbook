import * as React from "react";
import { GridList, GridTile, CardTitle, AutoComplete, TextField } from "material-ui";
import { ContentAdd } from "material-ui/svg-icons";
import { generate } from "uuidjs";
import { Chip } from "material-ui";

type Tag = { Id: string, Name: string };

interface RecipesFiltersProps {
    readonly tags: Models.TagDTO[];
    readonly onFiltersChanged: (text: string, tags: string[]) => void;
}

interface RecipesFiltersState {
    readonly text: string;
    readonly tag: string;
    readonly tags: Tag[];
}

export class RecipesFilters extends React.Component<RecipesFiltersProps, RecipesFiltersState> {
    constructor(props: RecipesFiltersProps) {
        super(props);

        this.state = {
            text: "",
            tag: "",
            tags: []
        };
    }

    private mapTags = (tags: Models.TagDTO[]) => {
        return tags.map(tag => tag.Name);
    }

    private changeSearchFilter = (text: string) => {
        this.setState({ text });
        this.props.onFiltersChanged(text, this.mapTags(this.state.tags));
    }

    private addTag = () => {
        let tag = { Id: generate(), Name: this.state.tag };
        let tags = this.state.tags;
        tags.push(tag);
        this.setState({ tags: tags, tag: "" });
        this.props.onFiltersChanged(this.state.text, this.mapTags(tags));
    }

    private removeTag = (index: number) => {
        let tags = this.state.tags;
        tags.splice(index, 1);
        this.setState({ tags });
        this.props.onFiltersChanged(this.state.text, this.mapTags(tags));
    }

    render() {
        return <div style={{ marginBottom: "3rem" }}>
            <TextField
                hintText="Search..."
                style={{ marginRight: "2rem" }}
                value={this.state.text}
                onChange={(_, text) => this.changeSearchFilter(text)} />

            <AutoComplete
                hintText="Filter by tags"
                openOnFocus={true}
                filter={AutoComplete.fuzzyFilter}
                dataSource={this.props.tags.map(tag => tag.Name)}
                searchText={this.state.tag}
                onUpdateInput={tag => this.setState({ tag })} />
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
        </div>;
    }
}
