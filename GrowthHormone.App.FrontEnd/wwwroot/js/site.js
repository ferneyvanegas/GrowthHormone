// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Función para habilitar un formulario diferente, según el tipo de usuario que se desee crear
function showForm(idForm){
    let forms = ['pacienteForm', 'especialistaForm', 'familiarForm'];
    forms.forEach(element => {
        if(element != idForm){
            document.getElementById(element).style.display = 'none';
        }
        else{
            document.getElementById(element).style.display = 'block';
        }
    });

    // Habilita el género intersexual para quienes no son Pacientes
    if(idForm == "pacienteForm")
        document.getElementById('Intersexual').style.display = 'none';
    else
        document.getElementById('Intersexual').style.display = 'block';
}

// Función para habilitar una lista diferente, según el tipo de usuario que se desee listar
function showList(idList, listName){
    let lists = ['pacienteList', 'especialistaList', 'familiarList'];
    lists.forEach(element => {
        if(element != idList){
            document.getElementById(element).style.display = 'none';
        }
        else{
            document.getElementById(element).style.display = 'block';
        }
    });

    document.getElementById('listTitle').innerHTML = listName; // Definir el título
    
    //Buscar pacientes
    if(listName == "Pacientes")
        document.getElementById("buscarPersona").style.display = 'block';
    else
        document.getElementById("buscarPersona").style.display = 'none';

    //Buscar Especialistas
    if(listName == "Especialistas")
        document.getElementById("buscarEspecialista").style.display = 'block';
    else
        document.getElementById("buscarEspecialista").style.display = 'none';
    
    // Fijas la lista buscada en el localStorage
    localStorage.setItem("lastListShowed", idList);
    localStorage.setItem("lastListNamed", listName);
}

// Función para almacenar en localStorage los datos de una persona a Editar
function saveToPassPersonData(personDocument, personName, personSurename, personPhone, personGender, personType, personId){
    localStorage.setItem("personDataSaved", "true");
    localStorage.setItem("personType", personType);
    localStorage.setItem("personId", personId);
    localStorage.setItem("personDocument", personDocument);
    localStorage.setItem("personName", personName);
    localStorage.setItem("personSurename", personSurename);
    localStorage.setItem("personPhone", personPhone);
    localStorage.setItem("personGender", personGender);
}

// Método que agrega hijos a la tabla de creación de Familiares y los agrega al formulario que se enviará para crear familiar
function addPacienteFamiliar(pacienteId, pacienteNombre, pacienteApellido){
    if(document.getElementById("parentesco'" + pacienteId + "'").value != ""){
        // Registro en el hidden de los valores que se han agregado
        // Id's de pacientes
        var ids = document.getElementById("ids").value;
        ids = ids + pacienteId + ",";
        document.getElementById("ids").value = ids;
        // Parentescos
        var parentescos = document.getElementById("parentescos").value;
        var parentesco = document.getElementById("parentesco'" + pacienteId + "'").value;
        parentescos = parentescos + parentesco + ",";
        document.getElementById("parentescos").value = parentescos;

        // Bloquear el botón
        document.getElementById("btnAddPaciente'"+ pacienteId + "'").disabled = true;
        document.getElementById("btnAddPaciente'"+ pacienteId + "'").classList.remove('btn-primary');
        document.getElementById("btnAddPaciente'"+ pacienteId + "'").classList.add('btn-danger');

        var table = document.getElementById('children');
        var row = table.insertRow(1);
        row.innerHTML = "<td>"
                            + "<input type='text' class='form-control' "
                            +"name='pac"+ pacienteId +"' " 
                            +"value='" + pacienteId + " - " + pacienteNombre +  " " + pacienteApellido 
                            + "' readonly>" 
                        + "</td>";
    }
    
}

function asignSpecialist(idSpecialist, specialty, specialistName){
    // console.log(idSpecialist + ' ' + specialty);
    switch(specialty){
        case "Endocrino":   document.getElementById('endocrino').value = idSpecialist;
                            document.getElementById('controlAddEndocrino').style.display = 'block';
                            document.getElementById('addEndocrinoName').innerHTML = specialistName;
                            break;
        case "Pediatra" :   document.getElementById('pediatra').value = idSpecialist;
                            document.getElementById('controlAddPediatra').style.display = 'block';
                            document.getElementById('addPediatraName').innerHTML = specialistName;
                            break;
    }    
}