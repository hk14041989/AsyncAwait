# AsyncAwait
- Example về async + await có kết hợp multi thread.
- Chia làm 2 #region:
+ #region multi thread + kết hợp async, await: chạy solution sẽ đưa ra kết quả là 1 luồng tính toán và 1 luồng ghi kết quả, luồng ghi kết
quả sẽ chờ luồng tính toán chạy xong rồi mới ghi ra kết quả đó.
+ #region single thread: comment phần #region multi thread lại, chạy solution để thấy sự khác biệt giữa việc có sử dụng async, await với
không sử dụng.
