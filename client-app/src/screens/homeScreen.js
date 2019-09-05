import React, {Component} from "react";
import {connect} from "react-redux";

class HomeScreen extends Component {
    render() {
        debugger;
        const {isAuthenticated,firstname,lastname} = this.props.user;
        return (
        <div>           
            <p> {isAuthenticated ? firstname + ' ' + lastname: "Home"} </p>
        </div>
        
        
        )
    }
}

const mapStateToProps = state => {
    return {
        user :state.user
    }
}
export default connect(mapStateToProps)(HomeScreen);