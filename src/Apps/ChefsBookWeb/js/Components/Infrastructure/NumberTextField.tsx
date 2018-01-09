import * as React from "react";
import { TextField, TextFieldProps } from "material-ui";

interface NumberTextFieldProps extends TextFieldProps {
    readonly onValueChange: (newValue: number) => void;
    readonly onValidationChange?: (isValid: boolean) => void;
    readonly value?: number;
}

interface NumberTextFieldState {
    readonly value: string;
}

export class NumberTextField extends React.PureComponent<NumberTextFieldProps, NumberTextFieldState> {
    constructor(props: NumberTextFieldProps) {
        super(props);

        this.state = {
            value: props.value ? props.value.toString() : ""
        };
    }

    componentDidMount() {
        this.props.onValidationChange && this.props.onValidationChange(this.props.disabled || !this.validationMessage());
    }

    componentWillReceiveProps(nextProps: NumberTextFieldProps) {
        if (!nextProps.value) {
            return;
        }

        if (this.props.value !== nextProps.value) {
            this.setState({ value: nextProps.value.toString() });
        }

        if (this.props.disabled !== nextProps.disabled && this.props.onValidationChange) {
            this.props.onValidationChange(nextProps.disabled || !this.validationMessage(nextProps.value.toString()));
        }
    }

    private onChange = (e: React.FormEvent<{}>, newValue: string) => {
        newValue = newValue.replace(/,/g, ".");

        let numberRegex: RegExp = /^[\-0-9\. ]*$/;

        if (numberRegex.test(newValue)) {
            if (this.props.onValidationChange) {
                let isValid = this.props.disabled || !this.validationMessage(newValue);
                let wasValid = this.props.disabled || this.validationMessage(newValue);

                if (isValid !== wasValid) {
                    this.props.onValidationChange(isValid);
                }
            }

            this.setState({ value: newValue });

            if (Number(newValue) !== this.props.value) {
                this.props.onValueChange(Number(newValue));
            }
        }
    }

    private validationMessage(value: string = this.state.value): string | undefined {
        let num = Number(value);
        return (isNaN(num) && "Invalid value") || undefined;
    }

    render() {
        let { onValueChange, onChange, onValidationChange, ...props } = this.props;

        return <TextField {...props} onChange={this.onChange} value={this.state.value} errorText={!this.props.disabled && this.validationMessage()}/>;
    }
}
