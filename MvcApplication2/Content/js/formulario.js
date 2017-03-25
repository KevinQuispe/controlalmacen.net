
function mostrar() {
    if (document.forminsumo.sel1.value != "1") {
        document.forminsumo.sel2.disabled = true;
    } else {
        if (document.forminsumo.sel1.value == "1") {
            document.forminsumo.sel2.disabled = false;
        }
    }
}
function habilitar(obj) {
    var hab;
    frm = obj.form;
    num = obj.selectedIndex;
    if (num == 2)

        hab = false;

    else if (num == 1)
        hab = true;
    frm.fechafin.disabled = hab;
}

function CallChangefunc(val) {
    var textof1 = document.getElementById("txtfechai");
    var textof2 = document.getElementById("txtfechaf");
    if (val == "1") {
        textof1.style.visibility = 'visible';
        textof2.style.visibility = 'hidden';
    }
    else {
        if (val == "2") {
            textof1.style.visibility = 'visible';
            textof2.style.visibility = 'visible';
        }
    }
}
function calligvvisible(val) {
    var textof1 = document.getElementById("igv1");
    var textof2 = document.getElementById("txttipodecambio");
    if (val == "1") {
        textof1.style.visibility = 'visible';
        textof2.value = '1';
        textof2.setAttribute("readonly", "readolny");
    }
    else {
        if (val == "2") {
            textof1.style.visibility = 'hidden';
            textof2.value = '';
            textof2.removeAttribute("readonly");
            document.getElementById("form1").submit();
        }
        else {
            textof1.style.visibility = 'visible';
            textof2.value = '';
        }
    }
}

function Calltipocambio() {
    var selec = document.getElementById("moneda").value;
    calligvvisible(selec);
}

function bloqueaform(val) {
    var fields = document.getElementById(val).getElementsByTagName('*');
    for (var i = 0; i < fields.length; i++) {
        fields[i].disabled = true;
    }
}
function bloquearbotones(val) {
    for (i = 0; i < val.length; i++) {
        document.getElementById(val[i]).disabled = true;
    } 
}
function ponerfocus(val) {
    document.getElementById(val).focus();
}

function bloqueafechaRC(val) {
    if (val == "1") {
        document.getElementById("txtfechaf").style.visibility = "hidden";
    } else {
        document.getElementById("txtfechaf").style.visibility = "visible";
    }   
}

function bloqtextbox(val) {
    if (val == "1") {
        document.getElementById("div2").style.display = "none";
        document.getElementById("div1").style.display = "block";
        document.getElementById("numGuia").value = "";
    } else {
        if (val == "2") {
            document.getElementById("div1").style.display = "none";            
            document.getElementById("numGuia").value = "Valor";
            document.getElementById("div2").style.display = "block";
        }
    }
}

function elijeLinea() {
    document.getElementById("form2").submit();
}


