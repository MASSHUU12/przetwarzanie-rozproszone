#include "read_matrix.h"
#include <iostream>

Matrix multiply(Matrix *m1, Matrix *m2) {
  Matrix m;
  m.items = new (std::nothrow) int32_t[m1->x * m1->y];
  m.x = m1->x;
  m.y = m1->y;

  m.items = new (std::nothrow) int32_t[m.y * m.x];
  if (m.items == nullptr) {
    std::cerr << "Error: Memory allocation failed" << std::endl;
    return m;
  }

  for (int32_t i = 0; i < m.y * m.x; ++i) {
    m.items[i] = 0;
  }

  for (int32_t i = 0; i < m1->y; ++i) {
    for (int32_t k = 0; k < m1->x; ++k) {
      int32_t a_ik = m1->items[i * m1->x + k];
      for (int32_t j = 0; j < m2->x; ++j) {
        m.items[i * m.x + j] += a_ik * m2->items[k * m2->x + j];
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

  std::string path_1 = argv[1];
  std::string path_2 = argv[2];

  Matrix m1 = read_matrix(path_1);
  Matrix m2 = read_matrix(path_2);
  Matrix m3 = multiply(&m1, &m2);

  std::cout << "x: " << m1.x << ' ' << "y: " << m1.y << '\n';

  for (int32_t i = 0; i < m3.y; i++) {
    for (int32_t j = 0; j < m3.x; j++) {
      std::cout << m3.items[i * m3.x + j] << " ";
    }
    std::cout << std::endl;
  }

  return 0;
}
