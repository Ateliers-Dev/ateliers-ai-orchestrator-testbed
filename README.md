# Ateliers AI Orchestrator Testbed

実Codex E2Eのための小さなC#/.NET教材です。初期状態には、意図的に残した単語数カウントの不具合と、それを検出する実行可能なチェックがあります。

## 安全と運用

- このリポジトリと課題は、Implementer・Reviewer・修正・再レビュー・push・PR・Reviewerコメント証跡のE2E確認だけに使います。
- 外部サービスへのデプロイ、秘密情報の扱い、実データの処理はありません。チェックはローカルで完結し、追加パッケージも使いません。
- 初期状態の不具合を修正したE2E用PRは、レビューと証跡確認の後にクローズしてください。**教材を再利用するため、その修正PRを `main` へマージしてはいけません。**
- 教材の説明・保守など、検証環境自体の変更は通常のレビュー可能なPRで提案します。この導入PRをマージしても、意図的な不具合と失敗するチェックは `main` に残ります。
- E2Eでは毎回 `main` から新しい作業ブランチを使い、教材 `main` を直接変更しないでください。

## 教材

`TextMetrics.CountWords` は空白で区切られた単語数を返す想定です。現在の実装は通常の半角スペースだけで分割するため、タブ・改行を含む入力を誤って数えます。

.NET 10 SDKでビルドします。

```powershell
dotnet build tests/WordCounter.Tests/WordCounter.Tests.csproj --configuration Release
```

課題チェックを実行します。

```powershell
dotnet run --project tests/WordCounter.Tests/WordCounter.Tests.csproj --configuration Release
```

初期状態では「tabs and newlines」と「whitespace only」の2チェックが失敗します。これは教材の再現確認で、ビルド自体は成功します。受入条件を満たす修正後は、全チェックが成功します。

## 実行シナリオ

課題の指示、受入条件、オーケストレーター既存契約への対応は [E2Eシナリオ](docs/e2e-scenario.md) を参照してください。エージェント作業の境界は [AGENTS.md](AGENTS.md) に記載しています。

シナリオでSlack通知を検証する場合の `disabled` / `test` / `auto` 選択、既定動作、設定ファイルの扱いも同シナリオに記載しています。Slack通知はオーケストレーター実行時だけに属し、課題コードやTestbed checkoutへ設定を追加しません。
