import { Menu } from 'antd';
import { home, signIn, signUp } from '../common/routes-directions';
import React from 'react';
import { withRouter } from 'react-router';
import { NavLink } from 'react-router-dom';

const Navbar = (props) => {
    return (
        <Menu theme="dark" mode="horizontal" selectedKeys={[props.location.pathname]}>
            <Menu.Item key={home}>
                <NavLink to={home} />
                Home
            </Menu.Item>
            <Menu.Item key={signIn}>
            <NavLink to={signIn} />
                Sign in
            </Menu.Item>
            <Menu.Item key={signUp}>
            <NavLink to={signUp} />
                Sign up
            </Menu.Item>
        </Menu>
    );
}

export default withRouter(Navbar);