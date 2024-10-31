import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import EmployeePage from './components/employee/EmployeePage';
import DepartmentPage from './components/department/DepartmentPage';

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/departments" element={<DepartmentPage/>} />
        <Route path="/employees" element={<EmployeePage />} />
      </Routes>
    </Router>
  );
}

export default App;
