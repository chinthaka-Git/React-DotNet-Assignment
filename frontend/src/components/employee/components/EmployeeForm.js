import React, { useEffect, useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { getDepartments } from "../../../api/departmentApi";

function EmployeeForm({ onSave, show, employee, isEdit, setShow }) {
  const [departments, setDepartments] = useState([]);
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    dateOfBirth: "",
    age: 0,
    salary: "",
    departmentId: "",
  });

  useEffect(() => {
    if (show) {
      fetchDepartments();

      if (isEdit && employee && employee.employeeId > 0) {
        setFormData({
          firstName: employee.firstName,
          lastName: employee.lastName,
          email: employee.email,
          dateOfBirth: formatDate(employee.dateOfBirth),
          age: calculateAge(employee.dateOfBirth),
          salary: employee.salary,
          departmentId: employee.department.departmentID || "",
        });
      } else {
        resetForm();
      }
    } else {
      resetForm();
    }
  }, [isEdit, employee]);

  const fetchDepartments = async () => {
    const response = await getDepartments();
    setDepartments(response.data);
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return `${date.getFullYear()}-${(date.getMonth() + 1)
      .toString()
      .padStart(2, "0")}-${date.getDate().toString().padStart(2, "0")}`;
  };

  const calculateAge = (dateOfBirth) => {
    const birthDate = new Date(dateOfBirth);
    const today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();

    const monthDiff = today.getMonth() - birthDate.getMonth();
    const dayDiff = today.getDate() - birthDate.getDate();
    if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0)) {
      age--;
    }
    return age;
  };

  const handleClose = () => {
    setShow(false);
    resetForm();
  };

  const resetForm = () => {
    setFormData({
      firstName: "",
      lastName: "",
      email: "",
      dateOfBirth: "",
      age: 0,
      salary: "",
      departmentId: "",
    });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "dateOfBirth") {
      setFormData({
        ...formData,
        [name]: value,
        age: calculateAge(value),
      });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave({
      employeeId: isEdit ? employee.employeeId : 0,
      ...formData,
      dateOfBirth: new Date(formData.dateOfBirth).toISOString(),
    });
    handleClose();
  };

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>{`${isEdit ? "Edit" : "Add"} Employee`}</Modal.Title>
      </Modal.Header>
      <Form onSubmit={handleSubmit}>
        <Modal.Body>
          <Form.Group>
            <Form.Label>First Name</Form.Label>
            <Form.Control
              type="text"
              name="firstName"
              value={formData.firstName}
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Last Name</Form.Label>
            <Form.Control
              type="text"
              name="lastName"
              value={formData.lastName}
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Date of Birth</Form.Label>
            <Form.Control
              type="date"
              name="dateOfBirth"
              value={formData.dateOfBirth}
              onChange={handleChange}
              required
              max={new Date().toISOString().split("T")[0]}
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Age</Form.Label>
            <Form.Control
              type="number"
              name="age"
              value={formData.age}
              readOnly
              disabled
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Salary</Form.Label>
            <Form.Control
              type="number"
              name="salary"
              value={formData.salary}
              onChange={handleChange}
              required
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Department</Form.Label>
            <Form.Control
              as="select"
              name="departmentId"
              value={formData.departmentId}
              onChange={handleChange}
              required
            >
              <option value="">Select Department</option>
              {departments.map((dept) => (
                <option key={dept.departmentID} value={dept.departmentID}>
                  {dept.departmentName}
                </option>
              ))}
            </Form.Control>
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" type="submit">
            Save Changes
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default EmployeeForm;
