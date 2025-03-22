#ifndef READ_MATRIX_H_
#define READ_MATRIX_H_

#include <cstdint>
#include <string>

struct Matrix {
  int32_t *items;
  int32_t x;
  int32_t y;
};

Matrix read_matrix(std::string path);

#endif // READ_MATRIX_H_
