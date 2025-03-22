#include "read_matrix.h"
#include <cstdint>
#include <cstring>
#include <iostream>

Matrix multiply(const Matrix *m1, const Matrix *m2) {
  Matrix m;

  if (m1->rows != m2->cols) {
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

  for (uint16_t i = 0; i < m1->cols; ++i) {
    for (uint16_t k = 0; k < m1->rows; ++k) {
      int32_t a_ik = m1->items[i * m1->rows + k];
      for (uint16_t j = 0; j < m2->rows; ++j) {
        m.items[i * m.rows + j] += a_ik * m2->items[k * m2->rows + j];
      }
    }
  }

  return m;
}

int main(int argc, char **argv) {
  if (argc < 3) {
    std::cerr << "Usage: <path_1> <path_2>\n";
    return 1;
  }

  const std::string path_1 = argv[1];
  const std::string path_2 = argv[2];

  const Matrix m1 = read_matrix(path_1);
  const Matrix m2 = read_matrix(path_2);

  if (m1.items == nullptr || m2.items == nullptr) {
    return 1;
  }

  const Matrix m3 = multiply(&m1, &m2);

  if (m3.items == nullptr) {
    return 1;
  }

  std::cout << "rows: " << m1.rows << ' ' << "columns: " << m1.cols << '\n';

  for (uint16_t i = 0; i < m3.cols; i++) {
    for (uint16_t j = 0; j < m3.rows; j++) {
      std::cout << m3.items[i * m3.rows + j] << " ";
    }
    std::cout << '\n';
  }

  delete[] m1.items;
  delete[] m2.items;
  delete[] m3.items;

  return 0;
}
