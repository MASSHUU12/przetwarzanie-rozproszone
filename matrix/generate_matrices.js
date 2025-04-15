import { writeFileSync } from "fs";

function generateMatrix(rows, cols) {
  const matrix = [];
  let value = 1;
  for (let i = 0; i < rows; i++) {
    const row = [];
    for (let j = 0; j < cols; j++) {
      row.push(value++);
    }
    matrix.push(row);
  }
  return matrix;
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

const args = process.argv.slice(2);
const rows = parseInt(args[0], 10) || 1000;
const cols = parseInt(args[1], 10) || 1000;
const matrixPath = args[2] || "./matrices/a.matrix";

const matrix = generateMatrix(rows, cols);

saveMatrixToFile(matrix, matrixPath);

console.log(`Matrix saved to ${matrixPath}`);
