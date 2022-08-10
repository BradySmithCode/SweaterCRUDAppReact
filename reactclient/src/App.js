import React, { useState, useEffect } from "react";
import Constants from "./utilities/Constants";
import SweaterCreateForm from "./components/SweaterCreateForm";
import SweaterUpdateForm from "./components/SweaterUpdateForm";

function App() {
  const [sweaters, setSweaters] = useState([]);

  const [showingCreateNewSweaterForm, setShowingCreateNewSweaterForm] =
    useState(false);

  const [sweaterCurrentlyBeingUpdated, setSweaterCurrentlyBeingUpdated] =
    useState(null);

  function getSweaters() {
    const url = Constants.API_URL_GET_ALL_SWEATERS;

    fetch(url, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((sweatersFromServer) => {
        setSweaters(sweatersFromServer);
      })
      .catch((error) => {
        alert(error);
      });
  }

  function deleteSweater(sweaterId) {
    const url = `${Constants.API_URL_DELETE_SWEATER_BY_ID}/${sweaterId}`;

    fetch(url, {
      method: "DELETE",
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        onSweaterDeleted(sweaterId);
      })
      .catch((error) => {
        alert(error);
      });
  }

  useEffect(() => {
    getSweaters();
  }, []);

  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {showingCreateNewSweaterForm === false &&
            sweaterCurrentlyBeingUpdated === null && (
              <div>
                <button
                  onClick={() => setShowingCreateNewSweaterForm(true)}
                  className="btn btn-dark btn-lg w-100 mt-4"
                >
                  Create New Sweater
                </button>
              </div>
            )}

          {sweaters.length > 0 &&
            showingCreateNewSweaterForm === false &&
            sweaterCurrentlyBeingUpdated === null &&
            renderSweatersTable()}

          {showingCreateNewSweaterForm && (
            <SweaterCreateForm onSweaterCreated={onSweaterCreated} />
          )}

          {sweaterCurrentlyBeingUpdated !== null && (
            <SweaterUpdateForm
              sweater={sweaterCurrentlyBeingUpdated}
              onSweaterUpdated={onSweaterUpdated}
            />
          )}
        </div>
      </div>
    </div>
  );

  function renderSweatersTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">SweaterId (PK)</th>
              <th scope="col">Manufacturer</th>
              <th scope="col">Quantity</th>
              <th scope="col">CRUD Operations</th>
            </tr>
          </thead>
          <tbody>
            {sweaters.map((sweater) => (
              <tr key={sweater.sweaterId}>
                <th scope="row">{sweater.sweaterId}</th>
                <td>{sweater.manufacturer}</td>
                <td>{sweater.quantity}</td>
                <td>
                  <button
                    onClick={() => setSweaterCurrentlyBeingUpdated(sweater)}
                    className="btn btn-dark btn-lg mx-3 my-3"
                  >
                    Update
                  </button>
                  <button
                    onClick={() => {
                      if (
                        window.confirm(
                          `Are you sure want to delete the sweater manufacturer ${sweater.manufacturer}`
                        )
                      )
                        deleteSweater(sweater.sweaterId);
                    }}
                    className="btn btn-secondary btn-lg"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }

  function onSweaterCreated(createdSweater) {
    setShowingCreateNewSweaterForm(false);

    if (createdSweater === null) {
      return;
    }

    alert(`${createdSweater.manufacturer} Sweater successfully created.`);
    getSweaters();
  }

  function onSweaterUpdated(updatedSweater) {
    setSweaterCurrentlyBeingUpdated(null);
    if (updatedSweater === null) {
      return;
    }

    let sweatersCopy = [...sweaters];

    const index = sweatersCopy.findIndex(
      (sweatersCopySweater, currentIndex) => {
        if (sweatersCopySweater.sweaterId === updatedSweater.sweaterId) {
          return true;
        }
      }
    );

    if (index !== -1) {
      sweatersCopy[index] = updatedSweater;
    }

    setSweaters(sweatersCopy);

    alert("Sweater successfully updated.");
  }

  function onSweaterDeleted(deletedSweaterId) {
    let sweatersCopy = [...sweaters];

    const index = sweatersCopy.findIndex(
      (sweatersCopySweater, currentIndex) => {
        if (sweatersCopySweater.sweaterId === deletedSweaterId) {
          return true;
        }
      }
    );

    if (index !== -1) {
      sweatersCopy.splice(index, 1);
    }

    setSweaters(sweatersCopy);

    alert("Sweater successfully removed.");
  }
}

export default App;
