import React from 'react'
import { render } from 'react-dom'
import { Button, Container, Header, Rating, Icon } from 'semantic-ui-react'
import { Mybuttom } from './somecomps/mybutton'

const MOUNT_NODE = document.getElementById('container')

const App = () => (
  <Container>
    <Mybuttom ></Mybuttom>
    {/*<Header as='h2'>Hello world!</Header>

    <Button
      content='Discover docs'
      href='https://react.semantic-ui.com'
      icon='github'
      labelPosition='left'
    />
    <Button size='small' color='green'>
      <Icon name='download' />
      Download
    </Button>
    <Rating rating={1} maxRating={5} />*/}
  </Container>
)

render(<App />, MOUNT_NODE)