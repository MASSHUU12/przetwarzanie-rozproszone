import { writeFileSync } from "fs";

function generateMatrixA(rows, cols) {
  const matrixA = [];
  let value = 1;
  for (let i = 0; i < rows; i++) {
    const row = [];
    for (let j = 0; j < cols; j++) {
      row.push(value++);
    }
    matrixA.push(row);
  }
  return matrixA;
}

function generateMatrixB(rows, cols) {
  const matrixB = [];
  let value = rows * cols;
  for (let i = 0; i < rows; i++) {
    const row = [];
    for (let j = 0; j < cols; j++) {
      row.push(value--);
    }
    matrixB.push(row);
  }
  return matrixB;
}

function saveMatrixToFile(matrix, filename) {
  const rows = matrix.length;
  const cols = matrix[0].length;
  const lines = [];

  lines.push(`${rows} ${cols}`);

  for (const row of matrix) {
    lines.push(row.join(" "));
  }

  writeFileSync(filename, lines.join("\n"));
}

const rows = 1000;
const cols = 1000;

const matrixA = generateMatrixA(rows, cols);
const matrixB = generateMatrixB(rows, cols);

saveMatrixToFile(matrixA, "a.matrix");
saveMatrixToFile(matrixB, "b.matrix");

console.log("Matrices saved to a.matrix and b.matrix");
