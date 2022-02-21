通過寫入快取的方式（判斷快取是否過期）來達到判斷是否同一個 Action 被同個用戶頻繁的呼叫
因為每個 Web 專案的回傳格式不盡相同，為了提昇元件重複利用性，將此 Filter 開放回傳的格式為 Abstract Method，由繼承者自行實作，將來可用在 Web, App API, 或不同的Web專案使用
# Rate Limit To Do List
- [x] 防止同一個 action 被同一用戶狂呼叫， 做一個 filter 來限制此種行為
- [x] 共用的 filter，由引用的 filter 決定超過速率後的處置，badrequest 改為 200 ，並且回應錯誤訊息

