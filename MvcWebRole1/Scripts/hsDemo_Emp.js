/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.8.24.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.2.0.debug.js" />
/// <reference path="modernizr-2.6.2.js" />

function employeeVM(id, name, title, location, phone, email, roles) {
    var self = this;

    self.Id = id;
    self.Name = name;
    self.Title = title;
    self.Location = location;
    self.Phone = phone;
    self.Email = email;
    self.Roles = roles;
    self.RoleName = ko.computed(function () {
        return this.Roles == 1 ? "Worker" : "HR";
    }, this);

    self.addCustomer = function () {
        $.ajax({
            url: "/api/employee/",
            type: 'post',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {
            }
        });
    }
}

function employeesVM() {
    var self = this;

    self.Employees = ko.observableArray([]);
    self.getEmployees = function () {
        self.Employees.removeAll();
        $.getJSON("/api/employee", function (data) {
            $.each(data, function (key, val) {
                self.Employees.push(new employeeVM(val.Id, val.Name, val.Title, val.Location, val.Phone, val.Email, val.Roles));
            });
        });
    };
}

function identityVM(name, password) {
    var self = this;
    self.Name = name;
    self.Password = password;
    self.login = function () {
        $.ajax({
            url: "/api/home",
            type: 'post',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {
            }
        });
    };
}

$(document).ready(function () {
    var employees = new employeesVM();
    employees.getEmployees();
    ko.applyBindings(employees, document.getElementById('createNode'));
});