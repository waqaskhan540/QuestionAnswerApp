import React from "react";
import { TextInput, ThemeContext } from "grommet"
import * as Colors from "../colors";

export const InputText = ({ id, name, placeholder, label, value, onChange, onClick, ...others }) => (
    <ThemeContext.Extend
        value={{
            global: {
                control: {
                    border:
                        { radius: "0px"}
                },
                focus : {
                    border : {
                        color : "#fff",                       
                    }
                }
            }
        }}
    >
        <TextInput
            id={id}
            name={name}
            placeholder={placeholder}
            label={label}
            onChange={onChange}
            value={value}
            onClick={onClick}
            focusIndicator={false}  
                      
            {...others}
            
            style = {InputStyles}

        />
    </ThemeContext.Extend>
)

const InputStyles = {
   borderLeft : "none",
   borderRight : "none",
   borderTop : "none",
   borderBottom : `1px solid ${Colors.TextInputBorderColor}`
}
