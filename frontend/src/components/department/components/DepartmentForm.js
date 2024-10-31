import React, { useEffect, useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";

function DepartmentForm({ onSave, show, department, isEdit, setShow }) {

  const [formData, setFormData] = useState({
    departmentCode: "",
    departmentName: "",
  });

  useEffect(() => {
    if (isEdit) {
      setFormData({
        departmentCode: department.departmentCode,
        departmentName: department.departmentName,
      });
    } else {
      setFormData({
        departmentCode: "",
        departmentName: "",
      });
    }
  }, [isEdit, department]);

  const handleClose = () => {
    setShow(false);
    setFormData({ departmentCode: "", departmentName: "" });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave({ departmentId: isEdit ? department.departmentId : 0, ...formData });
    handleClose();
  };

  return (
    <>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{`${isEdit ? 'Edit' : 'Add'} Department`}</Modal.Title>
        </Modal.Header>
        <Form onSubmit={handleSubmit}>
          <Modal.Body>
            <Form.Group>
              <Form.Label>Department Code</Form.Label>
              <Form.Control
                type="text"
                name="departmentCode"
                value={formData.departmentCode}
                onChange={handleChange}
                required
              />
            </Form.Group>
            <Form.Group>
              <Form.Label>Department Name</Form.Label>
              <Form.Control
                type="text"
                name="departmentName"
                value={formData.departmentName}
                onChange={handleChange}
                required
              />
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
    </>
  );
}

export default DepartmentForm;
