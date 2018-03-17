import * as React from "react";

import RefreshIndicator from "material-ui/RefreshIndicator";
import Dialog from "material-ui/Dialog";
import { connect } from "react-redux";

class LoadingIndicatorDisplay extends React.PureComponent<LoadingIndicatorStateProps> {
    render() {
        return <Dialog modal={true} open={this.props.isLoading} contentStyle={{ width: 1 }}>
            <RefreshIndicator size={50} left={-25} top={0} loadingColor="#FF9800" status="loading" />
        </Dialog>;
    }
}

interface LoadingIndicatorStateProps {
    readonly isLoading: boolean;
}

const mapStateToProps = (state: AsyncState) => {
    return {
        isLoading: state.tasksCount > 0
    };
};

export const LoadingIndicator = connect<LoadingIndicatorStateProps, {}, { }>(
    mapStateToProps
)(LoadingIndicatorDisplay);
