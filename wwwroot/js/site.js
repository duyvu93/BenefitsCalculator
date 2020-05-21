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

        if (el.FirstName && el.LastName && dependents.indexOf(el) !== -1) {
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
        '<input type="text" id="' + id + count + 'fname" class="form-control" onblur="setTotalCost()"/>' +
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

function setTotalCost() {
    var totalCosts = calculateEmployeeCost() + sumDependentCosts();
    var totalCostsDiv = document.getElementById('totalCost');
    totalCostsDiv.value = totalCosts;
    totalCostsDiv.innerHTML = '$' + totalCosts.toFixed(2);
}

function calculateEmployeeCost() {
    var firstName = document.getElementById('FirstName').value;
    var employeeCostDiv = document.getElementById('employeeCost');

    if (firstName.trim().toLowerCase().startsWith('a')) {
        employeeCostDiv.value = 900;
        employeeCostDiv.innerHTML = '$900.00';
    } else {
        employeeCostDiv.value = 1000;
        employeeCostDiv.innerHTML = '$1,000.00';
    }

    return employeeCostDiv.value;
}

function sumDependentCosts() {
    var cost = 0;

    cost += getDependentCosts('spouse');
    cost += getDependentCosts('child');

    document.getElementById('dependentCost').value = cost;
    document.getElementById('dependentCost').innerHTML = '$' + cost.toFixed(2);

    return cost;
}

function getDependentCosts(type) {
    var cost = 0;
    var name = ''

    for (i = 0; i < spouseCount; i++) {
        var el = document.getElementById(type + i + 'fname');
        name = el ? el.value : '';
        if (name.trim().toLowerCase().startsWith('a')) {
            cost += 450;
        } else if (name){
            cost += 500;
        }
    }

    return cost;
}