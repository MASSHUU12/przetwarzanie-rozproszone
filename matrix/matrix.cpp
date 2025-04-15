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

  file >> matrix.rows >> matrix.cols;

  if (file.fail() || matrix.rows <= MATRIX_SIZE_MIN ||
      matrix.rows > MATRIX_SIZE_MAX || matrix.cols <= MATRIX_SIZE_MIN ||
      matrix.cols > MATRIX_SIZE_MAX) {
    std::cerr << "Error: Invalid matrix dimensions\n";
    return matrix;
  }

  matrix.items = new (std::nothrow) int64_t[matrix.rows * matrix.cols];
  if (matrix.items == nullptr) {
    std::cerr << "Error: Buy more RAM.\n";
    return matrix;
  }

  for (uint16_t i = 0; i < matrix.rows; i++) {
    for (uint16_t j = 0; j < matrix.cols; j++) {
      file >> matrix.items[i * matrix.cols + j];
      if (file.fail()) {
        std::cerr << "Error: Failed to read matrix element at position (" << i
                  << ", " << j << ")\n";
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

  file << mat->rows << ' ' << mat->cols << '\n';

  for (uint16_t i = 0; i < mat->rows; i++) {
    for (uint16_t j = 0; j < mat->cols; j++) {
      file << mat->items[i * mat->cols + j];
      if (j < mat->cols - 1) {
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

  m.rows = m1->rows;
  m.cols = m2->cols;

  m.items = new (std::nothrow) int64_t[m.rows * m.cols];
  if (m.items == nullptr) {
    std::cerr << "Error: Memory allocation failed\n";
    return m;
  }

  std::memset(m.items, 0, sizeof(int64_t) * m.rows * m.cols);

#ifdef MATRIX_PARALLELIZE_FIRST
#pragma omp parallel for
#endif
  for (uint16_t i = 0; i < m1->rows; ++i) {
#ifdef MATRIX_PARALLELIZE_SECOND
#pragma omp parallel for
#endif
    for (uint16_t j = 0; j < m2->cols; ++j) {
#ifdef MATRIX_PARALLELIZE_THIRD
#pragma omp parallel for
#endif
      for (uint16_t k = 0; k < m1->cols; ++k) {
        m.items[i * m.cols + j] +=
            m1->items[i * m1->cols + k] * m2->items[k * m2->cols + j];
      }
    }
  }

  return m;
}

void matrix_print(const Matrix *m) {
  std::cout << "rows: " << m->rows << " columns: " << m->cols << '\n';
  for (uint16_t i = 0; i < m->rows; i++) {
    for (uint16_t j = 0; j < m->cols; j++) {
      std::cout << m->items[i * m->cols + j] << ' ';
    }
    std::cout << '\n';
  }
}
