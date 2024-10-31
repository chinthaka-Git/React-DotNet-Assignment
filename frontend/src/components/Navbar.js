import React from "react";
import { NavLink } from "react-router-dom";

function Navbar() {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container">
        <div className="navbar-brand mx-auto">Employee Management System</div>
        <div className="collapse navbar-collapse justify-content-center">
          <ul className="navbar-nav">
            <li className="nav-item mx-3">
              <NavLink
                to="/employees"
                className="nav-link"
                activeClassName="active"
                exact
              >
                Employees
              </NavLink>
            </li>
            <li className="nav-item mx-3">
              <NavLink
                to="/departments"
                className="nav-link"
                activeClassName="active"
              >
                Departments
              </NavLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
