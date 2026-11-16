# v7 実Codex E2Eシナリオ

## 目的

同じ未修正課題から毎回開始し、Ateliers AI Orchestrator の実Codex経路で Implementer、独立Reviewer、必要な場合の修正・再レビュー、push、PR作成、Reviewerコメント証跡まで確認します。修正ループを起こすこと自体は受入条件にしません。

この教材はローカルの文字列処理だけを扱います。外部サービス接続、デプロイ、秘密情報、実データはありません。E2Eで作成した修正PRは証跡を確認したらクローズし、`main` へマージしません。導入・保守のPRはレビュー対象の通常PRです。

## 対象課題と再現

対象は `src/WordCounter/TextMetrics.cs` の `TextMetrics.CountWords` です。単語の区切りとして半角スペースしか扱わず、タブと改行を含む入力を正しく数えません。

.NET 10 SDKで次を実行します。

```powershell
dotnet build tests/WordCounter.Tests/WordCounter.Tests.csproj --configuration Release
dotnet run --project tests/WordCounter.Tests/WordCounter.Tests.csproj --configuration Release
```

初期状態のビルドは成功し、チェック実行は `tabs and newlines` と `whitespace only` の2件で失敗します。外部NuGetパッケージはありません。

## オーケストレーター契約に渡す課題指示

既存のJSON契約を使い、独自の契約項目や独自スキーマは追加しないでください。現行契約のフィールド定義・制約は [オーケストレーターの契約テンプレート](https://github.com/Ateliers-Dev/ateliers-ai-orchestrator/blob/04ef555b5db44fdb68c325083c4d965df0f846f9/scripts/issue-17/task-contract.template.json) を参照してください。PR必須時の実行条件と証跡は [同コミットのREADME](https://github.com/Ateliers-Dev/ateliers-ai-orchestrator/blob/04ef555b5db44fdb68c325083c4d965df0f846f9/README.md) に記載されています。

次の内容を既存契約の該当フィールドに設定してください。これは値の指示であり、新しいJSONスキーマではありません。

- 作業ディレクトリは、E2E用にクローンしたこのリポジトリの絶対パス。
- 対象範囲は `src/WordCounter/` と `tests/WordCounter.Tests/`。目的は `CountWords` の不具合修正とチェックでの確認に限定。
- 要件: `CountWords` は空白文字で区切られた語を数える。連続する空白は空語として数えず、空文字列・空白のみは0を返す。既存の通常スペースの動作を維持する。
- 受入条件: 上記 `dotnet build` が成功する。チェック実行が終了コード0で、「ordinary spaces」「repeated spaces」「tabs and newlines」「empty input」「whitespace only」の全ケースを通過する。テストの期待値を変更・削除して通過させない。
- 委譲作業: 対象コードを調査・修正し、チェックを実行し、差分と結果を独立Reviewerへ渡す。指摘がある場合は契約の上限内で修正し再レビューする。指摘がなければ、不要な修正を加えず次へ進む。
- 有界性: 修正試行・再試行には小さな上限（例: 修正試行2回、再試行1回）を設定する。所要時間は既存契約が要求する形式の範囲内にする。
- 停止条件: 対象範囲外の変更、テスト仕様の曖昧さ、ビルドまたはチェックの再現不能、GitHub push/PRの権限不足、または教材 `main` を変更しなければ進められない状況。
- 公開条件: このシナリオではPRを期待する設定を使う（既存契約の `reporting.pullRequestExpected` を `true`）。push先は専用のE2E作業ブランチとし、PR作成後のReviewerコメントと実行証跡の整合を確認する。自動マージは行わない。

Implementerにはこの受入条件を満たす最小限の修正と検証を指示します。Reviewerには独立して差分・テスト結果・受入条件を照合させます。Reviewerの具体的な指摘が出ない場合に修正ループを強制してはいけません。

## 確認する証跡

- ImplementerとReviewerの実行結果、レビュー判定、実行記録が保存される。
- 必要な指摘があるときのみ、契約の上限内で修正・再レビューされる。
- PRの対象head/base SHAとレビュー差分が一致する。
- PRが作成され、PR本文に加えてReviewer結果コメントが投稿・照合される。
- 実行記録にPR URL、対象SHA、コメントの証跡が保存され、人間確認待ちで止まる。
- 検証用修正PRは `main` にマージせずクローズする。次回実行では再び `main` を起点にする。
