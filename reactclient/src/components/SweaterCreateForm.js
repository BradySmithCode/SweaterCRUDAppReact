import React, { useState } from "react";
import Constants from "../utilities/Constants";

export default function SweaterCreateForm(props) {
  const initialFormData = Object.freeze({
    manufacturer: "Sweater Name",
    quantity: 0,
  });
  const [formData, setFormData] = useState(initialFormData);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const sweaterToCreate = {
      sweaterId: 0,
      manufacturer: formData.manufacturer,
      quantity: formData.quantity,
    };

    const url = Constants.API_URL_CREATE_SWEATER;

    fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(sweaterToCreate),
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });

    props.onSweaterCreated(sweaterToCreate);
  };

  return (
    <form action="" className="w-100 px-5">
      <h1 className="mt-5">Create New Sweater</h1>

      <div className="mt-5">
        <label className="h3 form-label">Sweater Manufacturer</label>
        <input
          type="text"
          name="manufacturer"
          value={formData.manufacturer}
          className="form-control"
          onChange={handleChange}
        />
      </div>

      <div className="mt-4">
        <label className="h3 form-label">Sweater Quantity</label>
        <input
          type="text"
          name="quantity"
          value={formData.quantity}
          className="form-control"
          onChange={handleChange}
        />
      </div>

      <button onClick={handleSubmit} className="btn btn-dark btn-lg w-100 mt-5">
        Submit
      </button>
      <button
        onClick={() => props.onSweaterCreated(null)}
        className="btn btn-secondary btn-lg w-100 mt-3"
      >
        Cancel
      </button>
    </form>
  );
}
