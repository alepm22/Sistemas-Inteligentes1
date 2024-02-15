import sumar from "./sumador";
import multiplicar from "./multiplicacion";

const first = document.querySelector("#primer-numero");
const second = document.querySelector("#segundo-numero");
const form = document.querySelector("#operacion-form");
const div = document.querySelector("#resultado-div");

form.addEventListener('submit', function(event) {
  event.preventDefault();

  const firstNumber = Number.parseInt(first.value);
  const secondNumber = Number.parseInt(second.value);
  const submitButtonValue = event.submitter.value;

  let resultado;

  switch (submitButtonValue) {
    case 'sumar':
      resultado = sumar(firstNumber, secondNumber);
      break;
    case 'multiplicar':
      resultado = multiplicar(firstNumber, secondNumber);
      break;
    default:
      resultado = 'Operación no válida';
  }

  div.innerHTML = "<p>" + resultado + "</p>";
});

