import React from "react";
import { Button, Table } from "react-bootstrap";

function EmployeeTable({ employees, onEdit, onDelete }) {

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString("en-GB", {
      day: "numeric",
      month: "short",
      year: "numeric",
    });
  };

  return (
    <Table striped bordered hover className="mt-4">
      <thead className="text-center">
        <tr>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Email</th>
          <th>Date of Birth</th>
          <th>Salary</th>
          <th>Department</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {employees.map((employee) => (
          <tr key={employee.employeeId}>
            <td>{employee.firstName}</td>
            <td>{employee.lastName}</td>
            <td>{employee.email}</td>
            <td>{formatDate(employee.dateOfBirth)}</td>
            <td>{employee.salary}</td>
            <td>{employee.department.departmentName}</td>
            <td className="d-flex justify-content-center">
              <Button
                variant="warning"
                onClick={() => onEdit(employee)}
                className="me-3"
              >
                Edit
              </Button>
              <Button
                variant="danger"
                onClick={() => onDelete(employee.employeeId)}
              >
                Delete
              </Button>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}

export default EmployeeTable;
