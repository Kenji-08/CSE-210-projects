class Flashcard
{
    // Attributes

    // Array of all kana characters hiragana and katakana
    public string[] _kana = [
        "あ","い","う","え","お",
        "か","き","く","け","こ",
        "さ","し","す","せ","そ",
        "た","ち","つ","て","と",
        "な","に","ぬ","ね","の",
        "は","ひ","ふ","へ","ほ",
        "ま","み","む","め","も",
        "や",    "ゆ",    "よ",
        "ら","り","る","れ","ろ",
        "わ",            "を",
                "ん",
        "が","ぎ","ぐ","げ","ご",
        "ざ","じ","ず","ぜ","ぞ",
        "だ","ぢ","づ","で","ど",
        "ば","び","ぶ","べ","ぼ",
        "ぱ","ぴ","ぷ","ぺ","ぽ",

        "ア", "イ", "ウ", "エ", "オ",
        "カ", "キ", "ク", "ケ", "コ",
        "サ", "シ", "ス", "セ", "ソ",
        "タ", "チ", "ツ", "テ", "ト",
        "ナ", "ニ", "ヌ", "ネ", "ノ",
        "ハ", "ヒ", "フ", "ヘ", "ホ",
        "マ", "ミ", "ム", "メ", "モ",
        "ヤ",     "ユ",     "ヨ",
        "ラ", "リ", "ル", "レ", "ロ",
        "ワ",             "ヲ",
                 "ン",];
    // index of the first katakana character in the _kana array             
    string _katakanaIndex = 46;
    public string _kanji;
    public string _furigana;
    public string _english;
    public string _type;

    public string _example;
    public string _reading;
    public string _exampleKana;
    public string _exampleEnglish;

    // Behaviors
    public void Display()
    {
        
    }
}