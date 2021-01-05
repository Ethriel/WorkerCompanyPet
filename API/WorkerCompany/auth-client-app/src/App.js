import React, { Suspense } from 'react';
import './App.css';
import { Route, Switch } from 'react-router';
import { Layout } from 'antd';
import { home, signIn, signUp } from './components/common/routes-directions';

const { Header, Content, Footer } = Layout;

const HomePage = React.lazy(() => import('./components/home'));
const Navbar = React.lazy(() => import('./components/navbar'));
const Login = React.lazy(() => import('./components/sign-in/sign-in-component'));
const SignUp = React.lazy(() => import('./components/sign-up/sign-up-component'));

const App = () => {
  return (
    <Suspense fallback={<p>Loading...</p>}>

      <Layout className="layout" style={{ height: "100vh" }}>

        <Header>
          <Navbar />
        </Header>

        <Content style={{ padding: '0 50px' }}>
          <Switch>
            <Route exact path={home} render={() => <HomePage />} />
            <Route exact path={signIn} render={() => <Login />} />
            <Route exact path={signUp} render={() => <SignUp />} />

          </Switch>
        </Content>

        <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>

      </Layout>

    </Suspense>
  )
}

export default App;
