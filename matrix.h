#ifndef MATRIX_H_
#define MATRIX_H_

#include <cstdint>
#include <string>

#define MATRIX_PARALLELIZE

constexpr uint16_t MATRIX_SIZE_MIN = 0;
constexpr uint16_t MATRIX_SIZE_MAX = 100;

struct Matrix {
  int32_t *items;
  uint16_t rows;
  uint16_t cols;

  ~Matrix() {
    delete[] items;
  }
};

Matrix matrix_read(const std::string path);
bool matrix_save(const std::string path, const Matrix *matrix);
Matrix matrix_multiply(const Matrix *m1, const Matrix *m2);

#endif // MATRIX_H_
