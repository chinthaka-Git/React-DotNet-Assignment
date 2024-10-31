import React, { useState, useEffect } from "react";
import DepartmentForm from "./components/DepartmentForm";
import DepartmentTable from "./components/DepartmentTable";
import {
  getDepartments,
  addDepartment,
  updateDepartment,
  deleteDepartment,
} from "../../api/departmentApi";
import { Button } from "react-bootstrap";
import ConfirmDeleteModal from "../ConfirmDeleteModal";

function DepartmentPage() {
  const [departments, setDepartments] = useState([]);
  const [isEdit, setIsEdit] = useState(false);
  const [show, setShow] = useState(false);
  const [department, setDepartment] = useState(null);
  const [confirmDeleteShow, setConfirmDeleteShow] = useState(false);
  const [departmentToDelete, setDepartmentToDelete] = useState(null);

  useEffect(() => {
    fetchDepartments();
  }, []);

  const fetchDepartments = async () => {
    const response = await getDepartments();
    setDepartments(response.data);
  };

  const handleSave = async (data) => {
    await addDepartment(data);
    fetchDepartments();
    setShow(false);
  };

  const handleEdit = async (dep) => {
    await updateDepartment(department.departmentID, dep);
    fetchDepartments();
    setIsEdit(false);
    setShow(false); 
  };

  const onClickEdit = (department) => {
    setDepartment(department);
    setIsEdit(true);
    setShow(true); 
  };

  const handleDelete = async () => {
    if (departmentToDelete) {
        try {
            await deleteDepartment(departmentToDelete);
            fetchDepartments(); 
            setConfirmDeleteShow(false); 
            setDepartmentToDelete(null); 
        } catch (error) {
            if (error.response && error.response.data && error.response.data.message) {
                alert(error.response.data.message); 
            } else {
                alert("An unexpected error occurred.");
            }
        }
    }
};


  const handleShow = () => {
    setDepartment(null); 
    setIsEdit(false); 
    setShow(true);
  };

  const handleConfirmDelete = (id) => {
    setDepartmentToDelete(id);
    setConfirmDeleteShow(true);
  };

  return (
    <div className="container">
      <h2 className="mt-4">Departments</h2>
      <div className="d-flex justify-content-end">
        <Button variant="primary" onClick={handleShow}>
          Add Department
        </Button>
      </div>
      <DepartmentForm
        onSave={isEdit ? handleEdit : handleSave}
        show={show}
        isEdit={isEdit}
        department={department}
        setShow={setShow}
      />
      <DepartmentTable
        departments={departments}
        onEdit={onClickEdit}
        onDelete={handleConfirmDelete}
      />

      <ConfirmDeleteModal
        show={confirmDeleteShow}
        onHide={() => setConfirmDeleteShow(false)}
        onConfirm={handleDelete}
        name={
          departmentToDelete
            ? departments.find((dep) => dep.departmentID === departmentToDelete)
                ?.departmentName
            : ""
        }
      />
    </div>
  );
}

export default DepartmentPage;
