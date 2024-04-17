import React, { useState, useEffect } from "react";
import axios from "axios";
import { Container, Typography } from "@mui/material";
import ManageCustomers from "./Components/Customer/ManageCustomers";
import { Customer } from "./Types/Customer";
import CreateCustomer from "./Components/Customer/CreateCustomer";

const serverApiAddress: string = "http://localhost:52908/api/customer"

function App() {
  const [customers, setCustomers] = useState<Customer[]>([]);

  useEffect(() => {
    fetchCustomers();
  }, []);

  const fetchCustomers = () => {
    axios
      .get<Customer[]>(serverApiAddress + "/all")
      .then((response) => {
        setCustomers(response.data);
      })
      .catch((error) => {
        console.error("Error fetching customers: ", error);
      });
  };

  const createCustomer = (customer: Customer) => {
    axios
      .post(serverApiAddress, customer)
      .then(() => {
        fetchCustomers();
      })
      .catch((error) => {
        console.error("Error adding customer: ", error);
      });
  };

  const deleteCustomer = (id: number) => {
    axios
      .delete(`${serverApiAddress}/${id}`)
      .then(() => {
        fetchCustomers();
      })
      .catch((error) => {
        console.error("Error deleting customer: ", error);
      });
  };

  const updateCustomer = (updatedCustomer: Customer) => {
    axios
      .put(`${serverApiAddress}/${updatedCustomer.id}`, updatedCustomer)
      .then(() => {
        fetchCustomers();
      })
      .catch((error) => {
        console.error("Error updating customer: ", error);
      });
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Customers
      </Typography>
      <CreateCustomer createCustomer={createCustomer} />
      <ManageCustomers
        customers={customers}
        deleteCustomer={deleteCustomer}
        updateCustomer={updateCustomer}
      />
    </Container>
  );
}

export default App;
