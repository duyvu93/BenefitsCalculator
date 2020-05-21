var spouseCount = 1;
var childCount = 1;

function employeeCreate() {
    var dependents = [];

    dependents = dependents.concat(getDependents('spouse'));
    dependents = dependents.concat(getDependents('child'));

    $('#dependents').val(JSON.stringify(dependents));
    $('#newEmployeeForm').submit();
}

function getDependents(prefix) {
    var count = prefix === 'spouse' ? spouseCount : childCount;
    var dependents = [];

    for (i = 0; i < count; i++) {
        var el = {};
        el.FirstName = document.getElementById(prefix + i + 'fname').value;
        el.LastName = document.getElementById(prefix + i + 'lname').value;
        el.IsSpouse = prefix === 'spouse';

        if (el.FirstName && el.LastName) {
            dependents.push(el);
        }
    }

    return dependents;
}

function addNewRow(type) {
    var id = '';
    var count = 0;

    if (type === 'spouses') {
        id = 'spouse';
        count = spouseCount++;
    } else {
        id = 'child';
        count = childCount++;
    }

    var template = '<div class="row">' +
        '<div class="form-group">' +
        '<label for="' + id + count + 'fname" class="control-label">First Name</label>' +
        '<input type="text" id="' + id + count + 'fname" class="form-control" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label for="' + id + count + 'lname" class="control-label">Last Name</label>' +
        '<input type="text" id="' + id + count + 'lname" class="form-control" />' +
        '</div>' +
        '</div >';
    return template;
}

function addDependent(type) {
    var div = document.getElementById(type);
    var dependentCounter = document.getElementById('NumberOfDependents');
    dependentCounter.value = dependentCounter.value || 0; //undefined check, sets it to 0 if undefined

    if (dependentCounter.value < 9) {
        div.innerHTML += addNewRow(type);
        dependentCounter.value++;
    }
}