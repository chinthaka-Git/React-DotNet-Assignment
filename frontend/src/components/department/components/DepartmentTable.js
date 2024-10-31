import React from "react";
import { Button, Table } from "react-bootstrap";

function DepartmentTable({ departments, onEdit, onDelete }) {
  return (
    <Table striped bordered hover className="mt-4">
      <thead className="text-center">
        <tr>
          <th>Department Code</th>
          <th>Department Name</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {departments.map((department) => (
          <tr key={department.departmentId}>
            <td>{department.departmentCode}</td>
            <td>{department.departmentName}</td>
            <td className="d-flex justify-content-center">
              <div>
                <Button
                  variant="warning"
                  onClick={() => onEdit(department)}
                  className="me-3"
                >
                  Edit
                </Button>
              </div>
              <div>
                <Button
                  variant="danger"
                  onClick={() => onDelete(department.departmentID)}
                >
                  Delete
                </Button>
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}

export default DepartmentTable;
