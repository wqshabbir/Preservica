import React, { useState } from "react";
import { Button, TextField, Grid } from "@mui/material";
import { Customer } from "../../Types/Customer";

interface AddCustomerProps {
    createCustomer: (customer: Customer) => void;
}

function CreateCustomer({ createCustomer }: AddCustomerProps) {
    const [name, setName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");
    const [postalAddress, setPostalAddress] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!name || !phoneNumber || !email || !postalAddress) {
            alert("Please fill in all fields");
            return;
        }
        createCustomer({ name, phoneNumber, email, postalAddress });
        setName("");
        setPhoneNumber("");
        setEmail("");
        setPostalAddress("");
    };

    return (
        <form onSubmit={handleSubmit}>
            <Grid container spacing={2}>
                <Grid item xs={3}>
                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </Grid>
                <Grid item xs={3}>
                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Phone Number"
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                    />
                </Grid>
                <Grid item xs={3}>
                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Email Address"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </Grid>
                <Grid item xs={3}>
                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Postal Address"
                        value={postalAddress}
                        onChange={(e) => setPostalAddress(e.target.value)}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button type="submit" variant="contained" color="primary">
                        Add
                    </Button>
                </Grid>
            </Grid>
        </form>
    );
}

export default CreateCustomer;
