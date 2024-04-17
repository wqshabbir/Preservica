import React, { useState } from "react";
import {
    List,
    ListItem,
    ListItemText,
    Button,
    TextField,
    ListItemSecondaryAction,
} from "@mui/material";
import { Customer } from "../../Types/Customer";

interface CustomerListProps {
    customers: Customer[];
    deleteCustomer: (id: number) => void;
    updateCustomer: (customer: Customer) => void;
}

function ManageCustomers({
    customers,
    deleteCustomer,
    updateCustomer,
}: CustomerListProps) {
    const [editId, setEditId] = useState<number | null>(null);
    const [editedName, setEditedName] = useState("");
    const [editedPhoneNumber, setEditedPhoneNumber] = useState("");
    const [editedEmail, setEditedEmail] = useState("");
    const [editedPostalAddress, setEditedPostalAddress] = useState("");

    const handleCustomerEdit = (id: number) => {
        const customer = customers.find((c) => c.id === id);
        if (customer) {
            setEditId(id);
            setEditedName(customer.name);
            setEditedPhoneNumber(customer.phoneNumber);
            setEditedEmail(customer.email);
            setEditedPostalAddress(customer.postalAddress);
        }
    };

    const handleSave = () => {
        if (editId !== null) {
            updateCustomer({
                id: editId,
                name: editedName,
                phoneNumber: editedPhoneNumber,
                email: editedEmail,
                postalAddress: editedPostalAddress,
            });
            setEditId(null);
        }
    };

    const handleCancel = () => {
        setEditId(null);
    };

    return (
        <List>
            {customers.map((customer) => (
                <ListItem key={customer.id}>
                    {editId === customer.id ? (
                        <>
                            <TextField
                                value={editedName}
                                onChange={(e) => setEditedName(e.target.value)}
                            />
                            <TextField
                                value={editedPhoneNumber}
                                onChange={(e) => setEditedPhoneNumber(e.target.value)}
                            />
                            <TextField
                                value={editedEmail}
                                onChange={(e) => setEditedEmail(e.target.value)}
                            />
                            <TextField
                                value={editedPostalAddress}
                                onChange={(e) => setEditedPostalAddress(e.target.value)}
                            />
                            <Button onClick={handleSave}>Save</Button>
                            <Button onClick={handleCancel}>Cancel</Button>
                        </>
                    ) : (
                        <>
                            <ListItemText
                                primary={customer.name}
                                secondary={`Phone: ${customer.phoneNumber}, Email: ${customer.email}, Address: ${customer.postalAddress}`}
                            />
                            <ListItemSecondaryAction>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleCustomerEdit(customer.id!)}
                                >
                                    Edit
                                </Button>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    onClick={() => deleteCustomer(customer.id!)}
                                >
                                    Delete
                                </Button>
                            </ListItemSecondaryAction>
                        </>
                    )}
                </ListItem>
            ))}
        </List>
    );
}

export default ManageCustomers;
