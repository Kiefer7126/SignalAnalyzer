# SignalAnalzer
## BeatDetection.cs
### フロー
1. Spectral Fluxを求める
2. ピーク検出
3. ピークの自己相関を求める
4. 自己相関のピークを求める
5. 4のピークの間隔を求める
6. 5のビート間隔のビート系列を作成

### メソッド
* ExtractRisingComponent(double[][] 対象データ, int 開始周波数帯域(Hz), int 終了周波数帯域(Hz), int サンプリング周波数)
  - 指定した周波数帯域のSpectral Fluxを求める
* PeakDetection(double[] 対象データ)
  - 指定したデータのピーク時刻を検出
* CalcBeatInterval(double[] 対象データ)
* MakeBeat(int beatInterval, int beatLength, int startTime)

## ChorusStructure.cs
## ExportFile.cs
## FrequencyAnalyzer.cs
## GenerateWave.cs
## Graph.cs
## GroupingStructure.cs
## ImageProcessing.cs
## ImportFile.cs
## MainWindow.cs
## MatricalStructure.cs
## WavFile.cs
## WindowFunction.cs
