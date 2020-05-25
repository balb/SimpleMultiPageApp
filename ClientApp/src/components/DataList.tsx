import * as React from "react";

export const DataList: React.FC = () => {

    const [authorized, setAuthorized] = React.useState(false);
    const [values, setValues] = React.useState([]);

    React.useEffect(() => {
        fetch("/api/values")
            .then(response => {
                if (response.status == 200) {
                    setAuthorized(true);
                    response.json().then(data => {
                        setValues(data);
                    });
                }
            })
            .catch(error => {
                console.log("Error:", error);
            });
    }, []);

    return (<>
        {authorized && <ul>
            {values.map(x => (<li key={x}>{x}</li>))}
        </ul>}
        {!authorized && <h1>Unauthorized!</h1>}
    </>);
}
