#ifndef MATRIX_H_
#define MATRIX_H_

#include <cstdint>
#include <string>

constexpr uint16_t MATRIX_SIZE_MIN = 0;
constexpr uint16_t MATRIX_SIZE_MAX = 100;

struct Matrix {
  int32_t *items;
  uint16_t rows;
  uint16_t cols;
};

Matrix read_matrix(const std::string path);
bool save_matrix(const std::string path, const Matrix *matrix);

#endif // MATRIX_H_
