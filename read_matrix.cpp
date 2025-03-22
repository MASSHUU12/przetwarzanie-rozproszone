#include "read_matrix.h"
#include <cstdint>
#include <fstream>
#include <iostream>

Matrix read_matrix(const std::string path) {
  Matrix matrix;
  matrix.items = nullptr;
  matrix.rows = 0;
  matrix.cols = 0;

  std::ifstream file(path);
  if (!file.is_open()) {
    std::cerr << "Error: Unable to open file " << path << '\n';
    return matrix;
  }

  file >> matrix.cols >> matrix.rows;

  if (file.fail() || matrix.rows <= MATRIX_SIZE_MIN ||
      matrix.rows > MATRIX_SIZE_MAX || matrix.cols <= MATRIX_SIZE_MIN ||
      matrix.cols > MATRIX_SIZE_MAX) {
    std::cerr << "Error: Invalid matrix dimensions\n";
    return matrix;
  }

  matrix.items = new (std::nothrow) int32_t[matrix.rows * matrix.cols];
  if (matrix.items == nullptr) {
    std::cerr << "Error: Buy more RAM.\n";
    return matrix;
  }

  for (uint16_t y = 0; y < matrix.cols; y++) {
    for (uint16_t x = 0; x < matrix.rows; x++) {
      file >> matrix.items[y * matrix.rows + x];
      if (file.fail()) {
        std::cerr << "Error: Failed to read matrix element at position (" << y
                  << ", " << x << ")\n";
        delete[] matrix.items;
        matrix.items = nullptr;
        matrix.rows = 0;
        matrix.cols = 0;
        return matrix;
      }
    }
  }

  file.close();

  return matrix;
}
