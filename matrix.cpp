#include "matrix.h"
#include <cstdint>
#include <cstring>
#include <fstream>
#include <iostream>

Matrix matrix_read(const std::string path) {
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

bool matrix_save(const std::string path, const Matrix *mat) {
  std::ofstream file(path);
  if (!file.is_open()) {
    std::cerr << "Error: Unable to save to a file " << path << '\n';
    return false;
  }

  file << mat->cols << ' ' << mat->rows << '\n';

  for (uint16_t i = 0; i < mat->cols; i++) {
    for (uint16_t j = 0; j < mat->rows; j++) {
      file << mat->items[i * mat->rows + j];

      if (j < mat->rows - 1) {
        file << ' ';
      }
    }
    file << '\n';
  }

  file.close();

  return true;
}

Matrix matrix_multiply(const Matrix *m1, const Matrix *m2) {
  Matrix m;

  if (m1->cols != m2->rows) {
    std::cerr << "Error: Incompatible matrix dimensions for multiplication\n";
    m.items = nullptr;
    m.rows = 0;
    m.cols = 0;
    return m;
  }

  m.rows = m2->rows;
  m.cols = m1->cols;

  m.items = new (std::nothrow) int32_t[m.cols * m.rows];
  if (m.items == nullptr) {
    std::cerr << "Error: Memory allocation failed\n";
    return m;
  }

  std::memset(m.items, 0, sizeof(int32_t) * m.cols * m.rows);

#ifdef MATRIX_PARALLELIZE
#pragma omp parallel for
#endif
  for (uint16_t i = 0; i < m1->rows; ++i) {
    for (uint16_t j = 0; j < m2->cols; ++j) {
      for (uint16_t k = 0; k < m1->cols; ++k) {
        m.items[i * m.cols + j] +=
            m1->items[i * m1->cols + k] * m2->items[k * m2->cols + j];
      }
    }
  }

  return m;
}

void matrix_print(const Matrix *m) {
  std::cout << "rows: " << m->rows << ' ' << "columns: " << m->cols << '\n';

  for (uint16_t i = 0; i < m->cols; i++) {
    for (uint16_t j = 0; j < m->rows; j++) {
      std::cout << m->items[i * m->rows + j] << ' ';
    }
    std::cout << '\n';
  }
}
