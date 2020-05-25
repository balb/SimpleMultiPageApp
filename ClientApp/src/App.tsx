
import * as React from 'react';
import { DataList } from "./components/DataList"

interface Props {
  name: string
}

class App extends React.Component<Props> {
  render() {
    const { name } = this.props;
    return (<>
      <div>Hello {name}</div>
      <DataList />
    </>);
  }
}

export default App;
