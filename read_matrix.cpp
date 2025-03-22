#include "read_matrix.h"
#include <fstream>
#include <iostream>

Matrix read_matrix(std::string path) {
  Matrix matrix;
  matrix.items = nullptr;
  matrix.x = 0;
  matrix.y = 0;

  std::ifstream file(path);
  if (!file.is_open()) {
    std::cerr << "Error: Unable to open file " << path << '\n';
    return matrix;
  }

  file >> matrix.y >> matrix.x;

  if (file.fail() || matrix.x <= 0 || matrix.y <= 0) {
    std::cerr << "Error: Invalid matrix dimensions" << std::endl;
    return matrix;
  }

  matrix.items = new (std::nothrow) int32_t[matrix.x * matrix.y];
  if (matrix.items == nullptr) {
    std::cerr << "Error: Memory allocation failed" << std::endl;
    return matrix;
  }

  for (int32_t i = 0; i < matrix.y; i++) {
    for (int32_t j = 0; j < matrix.x; j++) {
      file >> matrix.items[i * matrix.x + j];
      if (file.fail()) {
        std::cerr << "Error: Failed to read matrix element at position (" << i
                  << ", " << j << ")" << std::endl;
        delete[] matrix.items;
        matrix.items = nullptr;
        matrix.x = 0;
        matrix.y = 0;
        return matrix;
      }
    }
  }

  file.close();

  return matrix;
}
