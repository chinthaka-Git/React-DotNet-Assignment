import React, { useState, useEffect } from "react";
import EmployeeForm from "./components/EmployeeForm";
import EmployeeTable from "./components/EmployeeTable";
import ConfirmDeleteModal from "../ConfirmDeleteModal";
import {
  getEmployees,
  addEmployee,
  updateEmployee,
  deleteEmployee,
} from "../../api/employeeApi";
import { Button } from "react-bootstrap";

function EmployeePage() {
  const [employees, setEmployees] = useState([]);
  const [isEdit, setIsEdit] = useState(false);
  const [show, setShow] = useState(false);
  const [confirmDeleteShow, setConfirmDeleteShow] = useState(false); 
  const [employee, setEmployee] = useState();
  const [employeeToDelete, setEmployeeToDelete] = useState(null);

  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    try {
      const response = await getEmployees();
      setEmployees(response.data);
    } catch (error) {
      console.error("Error fetching employees:", error);
    }
  };

  const handleSave = async (data) => {
    await addEmployee(data);
    fetchEmployees();
  };

  const handleEdit = async (emp) => {
    await updateEmployee(employee.employeeId, emp);
    fetchEmployees();
    setIsEdit(false);
  };

  const onClickEdit = (employee) => {
    setEmployee(employee);
    setIsEdit(true);
    setShow(true);
  };

  const handleDelete = async () => {
    if (employeeToDelete) {
      await deleteEmployee(employeeToDelete);
      fetchEmployees();
      setConfirmDeleteShow(false);
    }
  };

  const handleShow = () => setShow(true);

  const handleConfirmDelete = (id) => {
    setEmployeeToDelete(id); 
    setConfirmDeleteShow(true);
  };

  return (
    <div className="container">
      <h2 className="mt-4">Employees</h2>
      <div className="d-flex justify-content-end">
        <Button variant="primary" onClick={handleShow}>
          Add Employee
        </Button>
      </div>
      <EmployeeForm
        onSave={isEdit ? handleEdit : handleSave}
        show={show}
        isEdit={isEdit}
        employee={employee}
        setShow={setShow}
      />
      <EmployeeTable
        employees={employees}
        onEdit={onClickEdit}
        onDelete={handleConfirmDelete} 
      />
      
      <ConfirmDeleteModal
        show={confirmDeleteShow}
        onHide={() => setConfirmDeleteShow(false)}
        onConfirm={handleDelete}
        name={employeeToDelete ? employees.find(emp => emp.employeeId === employeeToDelete)?.firstName : ""}
      />
    </div>
  );
}

export default EmployeePage;
