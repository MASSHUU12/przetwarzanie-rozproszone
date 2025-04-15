#ifndef MATRIX_H_
#define MATRIX_H_

#include <cstdint>
#include <string>

#define MATRIX_PARALLELIZE_FIRST
// #define MATRIX_PARALLELIZE_SECOND
// #define MATRIX_PARALLELIZE_THIRD

constexpr uint16_t MATRIX_SIZE_MIN = 0;
constexpr uint16_t MATRIX_SIZE_MAX = 2000;

struct Matrix {
  int64_t *items;
  uint16_t rows;
  uint16_t cols;

  Matrix() : items(nullptr), rows(0), cols(0) {}

  // Copy constructor for deep copy.
  Matrix(const Matrix &other) : rows(other.rows), cols(other.cols) {
    size_t total_items = static_cast<size_t>(rows) * cols;
    items = new int64_t[total_items];
    std::copy(other.items, other.items + total_items, items);
  }

  Matrix &operator=(const Matrix &other) {
    if (this == &other)
      return *this;
    delete[] items;
    rows = other.rows;
    cols = other.cols;
    size_t total_items = static_cast<size_t>(rows) * cols;
    items = new int64_t[total_items];
    std::copy(other.items, other.items + total_items, items);
    return *this;
  }

  Matrix(Matrix &&other) noexcept : items(other.items), rows(other.rows), cols(other.cols) {
    other.items = nullptr;
    other.rows = other.cols = 0;
  }

  Matrix &operator=(Matrix &&other) noexcept {
    if (this == &other)
      return *this;
    delete[] items;
    items = other.items;
    rows = other.rows;
    cols = other.cols;
    other.items = nullptr;
    other.rows = other.cols = 0;
    return *this;
  }

  ~Matrix() {
    delete[] items;
  }
};

Matrix matrix_read(const std::string path);
bool matrix_save(const std::string path, const Matrix *mat);
Matrix matrix_multiply(const Matrix *m1, const Matrix *m2);
void matrix_print(const Matrix *m);

#endif // MATRIX_H_
