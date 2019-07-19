using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public static class Database
    {
        public static int VERSION = 1; // // 640 x 480から1024 x 768へ変更、プレイヤー位置情報互換
        public static int WIDTH_1024 = 1024; // 後編追加
        public static int WIDTH_OK_BUTTON = 120; // 後編追加
        public static int HEIGHT_768 = 768; // 後編追加
        public static int HEIGHT_MAIN_MESSAGE = 60; // 後編追加

        public static int MAX_SAVEDATA = 10; // セーブデータ最大保持数

        public static int DUNGEON_MOVE_LEN = 1; // 25; change unity
        public static int DUNGEON_BASE_X = 0;
        public static int DUNGEON_BASE_Y = 0;

        public static int FIRST_POS = 2; // 町へ戻る手前は2 絡みつくフランシス手前は325 ２階階段手前は598　Lizenos手前は82, ３階階段手前は22

        public static int MAX_BACKPACK_SIZE = 20; // 10->20 後編編集
        public static int MAX_ITEM_STACK_SIZE = 10; // 後編追加

        public static string NODE_MAINPLAYERSTATUS = @"MainPlayerStatus";
        public static string NODE_SECONDPLAYERSTATUS = @"SecondPlayerStatus";
        public static string NODE_THIRDPLAYERSTATUS = @"ThirdPlayerStatus";

        public static int MAX_GAMEDAY = 100;

        public static int MAX_ITEM_BANK = 100;

        public static int TOTAL_COMMAND_NUM = 150; // 後編追加 (8 + 42 + 24 + 45 + 30 + 1)
        public static int TOTAL_SPELL_NUM = 87; // 後編追加（42 + 45)
        public static int TOTAL_SKILL_NUM = 54; // 後編追加（24 + 30）

        public static int BATTLE_CORE_SLEEP = 0;
        public static int BASE_TIMER_BAR_LENGTH = 500;
        public static int BASE_TIMER_DIV = 60;
        public static int TIMER_ICON_NUM = 8;

        public static int TIMEUP_FIRST_RESPONSE = 600; // 後編追加
        public static int TIMEUP_COMPUTER_INTERRUPT = 400; // add unity

        // s 後編追加
        #region "キャラクター名称"
        public const string EIN_WOLENCE = @"アイン";
        public const string EIN_WOLENCE_FULL = @"アイン・ウォーレンス";

        public const string RANA_AMILIA = @"ラナ";
        public const string RANA_AMILIA_FULL = @"ラナ・アミリア";

        public const string VERZE_ARTIE = @"ヴェルゼ";
        public const string VERZE_ARTIE_FULL = @"ヴェルゼ・アーティ";

        public const string OL_LANDIS = @"ランディス";
        public const string OL_LANDIS_FULL = @"オル・ランディス";

        public const string SINIKIA_KAHLHANZ = @"カールハンツ";
        public const string SINIKIA_KAHLHANZ_FULL = @"シニキア・カールハンツ";
        #endregion
        #region "キャラクターカラー"
        public static float COLOR_EIN_R = 135 / 255.0f;
        public static float COLOR_EIN_G = 205 / 255.0f;
        public static float COLOR_EIN_B = 250 / 255.0f;

        public static float COLOR_RANA_R = 255 / 255.0f;
        public static float COLOR_RANA_G = 192 / 255.0f;
        public static float COLOR_RANA_B = 203 / 255.0f;

        public static float COLOR_VERZE_R = 192 / 255.0f;
        public static float COLOR_VERZE_G = 192 / 255.0f;
        public static float COLOR_VERZE_B = 192 / 255.0f;

        public static float COLOR_OL_R = 255 / 255.0f;
        public static float COLOR_OL_G = 215 / 255.0f;
        public static float COLOR_OL_B = 0 / 255.0f;

        public static float COLOR_KAHL_R = 106 / 255.0f;
        public static float COLOR_KAHL_G = 90 / 255.0f;
        public static float COLOR_KAHL_B = 205 / 255.0f;

        public static float COLOR_BOX_EIN_R = 0 / 255.0f;
        public static float COLOR_BOX_EIN_G = 255 / 255.0f;
        public static float COLOR_BOX_EIN_B = 255 / 255.0f;

        public static float COLOR_BOX_RANA_R = 255 / 255.0f;
        public static float COLOR_BOX_RANA_G = 0 / 255.0f;
        public static float COLOR_BOX_RANA_B = 0 / 255.0f;

        public static float COLOR_BOX_VERZE_R = 128 / 255.0f;
        public static float COLOR_BOX_VERZE_G = 128 / 255.0f;
        public static float COLOR_BOX_VERZE_B = 128 / 255.0f;

        public static float COLOR_BOX_OL_R = 255 / 255.0f;
        public static float COLOR_BOX_OL_G = 255 / 255.0f;
        public static float COLOR_BOX_OL_B = 0 / 255.0f;

        public static float COLOR_BOX_KAHL_R = 128 / 255.0f;
        public static float COLOR_BOX_KAHL_G = 0 / 255.0f;
        public static float COLOR_BOX_KAHL_B = 128 / 255.0f;

        public static float COLOR_BATTLE_EIN_R = 0 / 255.0f;
        public static float COLOR_BATTLE_EIN_G = 191 / 255.0f;
        public static float COLOR_BATTLE_EIN_B = 255 / 255.0f;

        public static float COLOR_BATTLE_RANA_R = 238 / 255.0f;
        public static float COLOR_BATTLE_RANA_G = 130 / 255.0f;
        public static float COLOR_BATTLE_RANA_B = 238 / 255.0f;

        public static float COLOR_BATTLE_VERZE_R = 112 / 255.0f;
        public static float COLOR_BATTLE_VERZE_G = 128 / 255.0f;
        public static float COLOR_BATTLE_VERZE_B = 144 / 255.0f;

        public static float COLOR_BATTLE_OL_R = 255 / 255.0f;
        public static float COLOR_BATTLE_OL_G = 255 / 255.0f;
        public static float COLOR_BATTLE_OL_B = 0 / 255.0f;

        public static float COLOR_BATTLE_KAHL_R = 106 / 255.0f;
        public static float COLOR_BATTLE_KAHL_G = 90 / 255.0f;
        public static float COLOR_BATTLE_KAHL_B = 205 / 255.0f;

        public static float COLOR_BATTLE_TARGET1_EIN_R = 0 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_EIN_G = 0 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_EIN_B = 139 / 255.0f;

        public static float COLOR_BATTLE_TARGET1_RANA_R = 255 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_RANA_G = 105 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_RANA_B = 180 / 255.0f;

        public static float COLOR_BATTLE_TARGET1_VERZE_R = 112 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_VERZE_G = 128 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_VERZE_B = 144 / 255.0f;

        public static float COLOR_BATTLE_TARGET1_OL_R = 255 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_OL_G = 215 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_OL_B = 0 / 255.0f;

        public static float COLOR_BATTLE_TARGET1_KAHL_R = 72 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_KAHL_G = 61 / 255.0f;
        public static float COLOR_BATTLE_TARGET1_KAHL_B = 139 / 255.0f;

        public static float COLOR_BATTLE_TARGET2_EIN_R = 0 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_EIN_G = 0 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_EIN_B = 255 / 255.0f;

        public static float COLOR_BATTLE_TARGET2_RANA_R = 255 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_RANA_G = 192 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_RANA_B = 203 / 255.0f;

        public static float COLOR_BATTLE_TARGET2_VERZE_R = 211 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_VERZE_G = 211 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_VERZE_B = 211 / 255.0f;

        public static float COLOR_BATTLE_TARGET2_OL_R = 255 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_OL_G = 255 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_OL_B = 0 / 255.0f;

        public static float COLOR_BATTLE_TARGET2_KAHL_R = 106 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_KAHL_G = 90 / 255.0f;
        public static float COLOR_BATTLE_TARGET2_KAHL_B = 205 / 255.0f;
        #endregion
        // e 後編追加

        // s 後編追加
        public const string STRING_LIGHT = @"【聖耐性】";
        public const string STRING_SHADOW = @"【闇耐性】";
        public const string STRING_FIRE = @"【火耐性】";
        public const string STRING_ICE = @"【水耐性】";
        public const string STRING_FORCE = @"【理耐性】";
        public const string STRING_WILL = @"【空耐性】";
        public const string STRING_ACTIVE = @"【動】";
        public const string STRING_PASSIVE = @"【静】";
        public const string STRING_SOFT = @"【柔】";
        public const string STRING_HARD = @"【剛】";
        public const string STRING_TRUTH = @"【心眼】";
        public const string STRING_VOID = @"【無心】";

        public const string STRING_STUNNING = @"【スタン】";
        public const string STRING_SILENCE = @"【沈黙】";
        public const string STRING_POISON = @"【猛毒】";
        public const string STRING_TEMPTATION = @"【誘惑】";
        public const string STRING_FROZEN = @"【凍結】";
        public const string STRING_PARALYZE = @"【麻痺】";
        public const string STRING_SLOW = @"【鈍化】";
        public const string STRING_BLIND = @"【暗闇】";
        public const string STRING_SLIP = @"【スリップ】";
        public const string STRING_NORESURRECTION = @"【蘇生不可】";
        public const string STRING_NOGAIN_LIFE = @"【ライフ回復不可】";
        // e 後編追加

        public static string POWER_STR = @"【力】";
        public static string POWER_AGL = @"【技】";
        public static string POWER_INT = @"【知】";
        public static string POWER_STM = @"【体】";
        public static string POWER_MND = @"【心】";

        #region "魔法名称"
        // [聖]
        public const string FRESH_HEAL = @"FreshHeal";
        public const string PROTECTION = @"Protection";
        public const string HOLY_SHOCK = @"HolyShock";
        public const string SAINT_POWER = @"SaintPower";
        public const string GLORY = @"Glory";
        public const string RESURRECTION = @"Resurrection";
        public const string CELESTIAL_NOVA = @"CelestialNova";
        // [闇]
        public const string DARK_BLAST = @"DarkBlast";
        public const string SHADOW_PACT = @"ShadowPact";
        public const string LIFE_TAP = @"LifeTap"; // change unity
        public const string BLACK_CONTRACT = @"BlackContract";
        public const string DEVOURING_PLAGUE = @"DevouringPlague"; // e 後編編集（スペルミス）
        public const string BLOODY_VENGEANCE = @"BloodyVengeance";
        public const string DAMNATION = @"Damnation";
        // [火]
        public const string FIRE_BALL = @"FireBall";
        public const string FLAME_AURA = @"FlameAura";
        public const string HEAT_BOOST = @"HeatBoost";
        public const string FLAME_STRIKE = @"FlameStrike";
        public const string VOLCANIC_WAVE = @"VolcanicWave";
        public const string IMMORTAL_RAVE = @"ImmortalRave";
        public const string LAVA_ANNIHILATION = @"LavaAnnihilation";
        // [水]
        public const string ICE_NEEDLE = @"IceNeedle";
        public const string ABSORB_WATER = @"AbsorbWater";
        public const string CLEANSING = @"Cleansing";
        public const string FROZEN_LANCE = @"FrozenLance";
        public const string MIRROR_IMAGE = @"MirrorImage";
        public const string PROMISED_KNOWLEDGE = @"PromisedKnowledge";
        public const string ABSOLUTE_ZERO = @"AbsoluteZero";
        // [理]
        public const string WORD_OF_POWER = @"WordOfPower";
        public const string GALE_WIND = @"GaleWind";
        public const string WORD_OF_LIFE = @"WordOfLife";
        public const string WORD_OF_FORTUNE = @"WordOfFortune";
        public const string AETHER_DRIVE = @"AetherDrive";
        public const string GENESIS = @"Genesis";
        public const string ETERNAL_PRESENCE = @"EternalPresence";
        // [空]
        public const string DISPEL_MAGIC = @"DispelMagic";
        public const string RISE_OF_IMAGE = @"RiseOfImage";
        public const string DEFLECTION = @"Deflection";
        public const string TRANQUILITY = @"Tranquility";
        public const string ONE_IMMUNITY = @"OneImmunity";
        public const string WHITE_OUT = @"WhiteOut";
        public const string TIME_STOP = @"TimeStop";

        // s 後編追加
        // [聖]
        public const string FRESH_HEAL_JP = @"フレッシュ・ヒール";
        public const string PROTECTION_JP = @"プロテクション";
        public const string HOLY_SHOCK_JP = @"ホーリー・ショック";
        public const string SAINT_POWER_JP = @"セイント・パワー";
        public const string GLORY_JP = @"グローリー";
        public const string RESURRECTION_JP = @"リザレクション";
        public const string CELESTIAL_NOVA_JP = @"セレスティアル・ノヴァ";
        // [闇]
        public const string DARK_BLAST_JP = @"ダーク・ブラスト";
        public const string SHADOW_PACT_JP = @"シャドウ・パクト";
        public const string LIFE_TAP_JP = @"ライフ・タップ";
        public const string BLACK_CONTRACT_JP = @"ブラック・コントラクト";
        public const string DEVOURING_PLAGUE_JP = @"デヴォリング・プラグー";
        public const string BLOODY_VENGEANCE_JP = @"ブラッディ・ヴェンジェンス";
        public const string DAMNATION_JP = @"ダムネーション";
        // [火]
        public const string FIRE_BALL_JP = @"ファイア・ボール";
        public const string FLAME_AURA_JP = @"フレイム・オーラ";
        public const string HEAT_BOOST_JP = @"ヒート・ブースト";
        public const string FLAME_STRIKE_JP = @"フレイム・ストライク";
        public const string VOLCANIC_WAVE_JP = @"ヴォルカニック・ウェイブ";
        public const string IMMORTAL_RAVE_JP = @"イモータル・レイブ";
        public const string LAVA_ANNIHILATION_JP = @"ラヴァ・アニヒレーション";
        // [水]
        public const string ICE_NEEDLE_JP = @"アイス・ニードル";
        public const string ABSORB_WATER_JP = @"アブソーブ・ウォーター";
        public const string CLEANSING_JP = @"クリーンジング";
        public const string FROZEN_LANCE_JP = @"フローズン・ランス";
        public const string MIRROR_IMAGE_JP = @"ミラー・イメージ";
        public const string PROMISED_KNOWLEDGE_JP = @"プロミスド・ナレッジ";
        public const string ABSOLUTE_ZERO_JP = @"アブソリュート・ゼロ";
        // [理]
        public const string WORD_OF_POWER_JP = @"ワード・オブ・パワー";
        public const string GALE_WIND_JP = @"ゲイル・ウィンド";
        public const string WORD_OF_LIFE_JP = @"ワード・オブ・ライフ";
        public const string WORD_OF_FORTUNE_JP = @"ワード・オブ・フォーチュン";
        public const string AETHER_DRIVE_JP = @"エーテル・ドライブ";
        public const string GENESIS_JP = @"ジェネシス";
        public const string ETERNAL_PRESENCE_JP = @"エターナル・プリゼンス";
        // [空]
        public const string DISPEL_MAGIC_JP = @"ディスペル・マジック";
        public const string RISE_OF_IMAGE_JP = @"ライズ・オブ・イメージ";
        public const string DEFLECTION_JP = @"デフレクション";
        public const string TRANQUILITY_JP = @"トランキィリティ";
        public const string ONE_IMMUNITY_JP = @"ワン・イムーニティ";
        public const string WHITE_OUT_JP = @"ホワイト・アウト";
        public const string TIME_STOP_JP = @"タイムストップ";
        // e 後編追加

        // s 後編追加
        // [聖　闇]（完全逆）
        public const string PSYCHIC_TRANCE = @"PsychicTrance";
        public const string PSYCHIC_TRANCE_JP = @"サイキック・トランス";
        public const string BLIND_JUSTICE = @"BlindJustice";
        public const string BLIND_JUSTICE_JP = @"ブラインド・ジャスティス";
        public const string TRANSCENDENT_WISH = @"TranscendentWish";
        public const string TRANSCENDENT_WISH_JP = @"トラッセンデント・ウィッシュ";

        // [聖　火]
        public const string FLASH_BLAZE = @"FlashBlaze";
        public const string FLASH_BLAZE_JP = @"フラッシュ・ブレイズ";
        public const string LIGHT_DETONATOR = @"LightDetonator";
        public const string LIGHT_DETONATOR_JP = @"ライト・デトネイター";
        public const string ASCENDANT_METEOR = @"AscendantMeteor";
        public const string ASCENDANT_METEOR_JP = @"アセンダント・メテオ";

        // [聖　水]
        public const string SKY_SHIELD = @"SkyShield";
        public const string SKY_SHIELD_JP = @"スカイ・シールド";
        public const string SACRED_HEAL = @"SacredHeal";
        public const string SACRED_HEAL_JP = @"サークレッド・ヒール";
        public const string EVER_DROPLET = @"EverDroplet";
        public const string EVER_DROPLET_JP = @"エヴァー・ドロップレット";

        // [聖　理]
        public const string HOLY_BREAKER = @"HolyBreaker";
        public const string HOLY_BREAKER_JP = @"ホーリー・ブレイカー";
        public const string EXALTED_FIELD = @"ExaltedField";
        public const string EXALTED_FIELD_JP = @"エグザルティッド・フィールド";
        public const string HYMN_CONTRACT = @"HymnContract";
        public const string HYMN_CONTRACT_JP = @"ヒムン・コントラクト";

        // [聖　空]
        public const string STAR_LIGHTNING = @"StarLightning";
        public const string STAR_LIGHTNING_JP = @"スター・ライトニング";
        public const string ANGEL_BREATH = @"AngelBreath";
        public const string ANGEL_BREATH_JP = @"エンジェル・ブレス";
        public const string ENDLESS_ANTHEM = @"EndlessAnthem";
        public const string ENDLESS_ANTHEM_JP = @"エンドレス・アンセム";

        // [闇　火]
        public const string BLACK_FIRE = @"BlackFire";
        public const string BLACK_FIRE_JP = @"ブラック・ファイア";
        public const string BLAZING_FIELD = @"BlazingField";
        public const string BLAZING_FIELD_JP = @"ブレイジング・フィールド";
        public const string DEMONIC_IGNITE = @"DemonicIgnite";
        public const string DEMONIC_IGNITE_JP = @"デーモニック・イグナイト";

        // [闇　水]
        public const string BLUE_BULLET = @"BlueBullet";
        public const string BLUE_BULLET_JP = @"ブルー・バレット";
        public const string DEEP_MIRROR = @"DeepMirror";
        public const string DEEP_MIRROR_JP = @"ディープ・ミラー";
        public const string DEATH_DENY = @"DeathDeny";
        public const string DEATH_DENY_JP = @"デス・ディナイ";

        // [闇　理]
        public const string WORD_OF_MALICE = @"WordOfMalice";
        public const string WORD_OF_MALICE_JP = @"ワード・オブ・マリス";
        public const string ABYSS_EYE = @"AbyssEye";
        public const string ABYSS_EYE_JP = @"アビス・アイ";
        public const string SIN_FORTUNE = @"SinFortune";
        public const string SIN_FORTUNE_JP = @"シン・フォーチュン";

        // [闇　空]
        public const string DARKEN_FIELD = @"DarkenField";
        public const string DARKEN_FIELD_JP = @"ダーケン・フィールド";
        public const string DOOM_BLADE = @"DoomBlade";
        public const string DOOM_BLADE_JP = @"ドゥーム・ブレイド";
        public const string ECLIPSE_END = @"EclipseEnd";
        public const string ECLIPSE_END_JP = @"エクリプス・エンド";

        // [火　水]（完全逆）
        public const string FROZEN_AURA = @"FrozenAura";
        public const string FROZEN_AURA_JP = @"フローズン・オーラ";
        public const string CHILL_BURN = @"ChillBurn";
        public const string CHILL_BURN_JP = @"チル・バーン";
        public const string ZETA_EXPLOSION = @"ZetaExplosion";
        public const string ZETA_EXPLOSION_JP = @"ゼータ・エクスプロージョン";

        // [火　理]
        public const string ENRAGE_BLAST = @"EnrageBlast";
        public const string ENRAGE_BLAST_JP = @"エンレイジ・ブラスト";
        public const string PIERCING_FLAME = @"PiercingFlame";
        public const string PIERCING_FLAME_JP = @"ピアーシング・フレイム";
        public const string SIGIL_OF_HOMURA = @"SigilOfHomura";
        public const string SIGIL_OF_HOMURA_JP = @"焔の刻印";

        // [火　空]
        public const string IMMOLATE = @"Immolate";
        public const string IMMOLATE_JP = @"イモーレイト";
        public const string PHANTASMAL_WIND = @"PhantasmalWind";
        public const string PHANTASMAL_WIND_JP = @"ファンタズマル・ウィンド";
        public const string RED_DRAGON_WILL = @"RedDragonWill";
        public const string RED_DRAGON_WILL_JP = @"レッド・ドラゴン・ウィル";

        // [水　理]
        public const string WORD_OF_ATTITUDE = @"WordOfAttitude";
        public const string WORD_OF_ATTITUDE_JP = @"ワード・オブ・アティチュード";
        public const string STATIC_BARRIER = @"StaticBarrier";
        public const string STATIC_BARRIER_JP = @"スタティック・バリア";
        public const string AUSTERITY_MATRIX = @"AusterityMatrix";
        public const string AUSTERITY_MATRIX_JP = @"アゥステリティ・マトリクス";

        // [水　空]
        public const string VANISH_WAVE = @"VanishWave";
        public const string VANISH_WAVE_JP = @"ヴァニッシュ・ウェイヴ";
        public const string VORTEX_FIELD = @"VortexField";
        public const string VORTEX_FIELD_JP = @"ヴォルテクス・フィールド";
        public const string BLUE_DRAGON_WILL = @"BlueDragonWill";
        public const string BLUE_DRAGON_WILL_JP = @"ブルー・ドラゴン・ウィル";

        // [理　空]（完全逆）
        public const string SEVENTH_MAGIC = @"SeventhMagic";
        public const string SEVENTH_MAGIC_JP = @"セブンス・マジック";
        public const string PARADOX_IMAGE = @"ParadoxImage";
        public const string PARADOX_IMAGE_JP = @"パラドックス・イメージ";
        public const string WARP_GATE = @"WarpGate";
        public const string WARP_GATE_JP = @"ワープ・ゲート";
        // e 後編追加
        #endregion

        #region "スキル名称"
        // [動]
        public const string STRAIGHT_SMASH = @"StraightSmash";
        public const string DOUBLE_SLASH = @"DoubleSlash";
        public const string CRUSHING_BLOW = @"CrushingBlow";
        public const string SOUL_INFINITY = @"SoulInfinity";
        // [静]
        public const string COUNTER_ATTACK = @"CounterAttack";
        public const string PURE_PURIFICATION = @"PurePurification";
        public const string ANTI_STUN = @"AntiStun";
        public const string STANCE_OF_DEATH = @"StanceOfDeath";
        // [柔]
        public const string STANCE_OF_FLOW = @"StanceOfFlow";
        public const string ENIGMA_SENSE = @"EnigmaSense";
        public const string SILENT_RUSH = @"SilentRush";
        public const string OBORO_IMPACT = @"OboroImpact";
        // [剛]
        public const string STANCE_OF_STANDING = @"StanceOfStanding";
        public const string INNER_INSPIRATION = @"InnerInspiration";
        public const string KINETIC_SMASH = @"KineticSmash";
        public const string CATASTROPHE = @"Catastrophe";
        // [心眼]
        public const string TRUTH_VISION = @"TruthVision";
        public const string HIGH_EMOTIONALITY = @"HighEmotionality";
        public const string STANCE_OF_EYES = @"StanceOfEyes";
        public const string PAINFUL_INSANITY = @"PainfulInsanity";
        // [無心]
        public const string NEGATE = @"Negate"; // e 後編編集
        public const string VOID_EXTRACTION = @"VoidExtraction";
        public const string CARNAGE_RUSH = @"CarnageRush";
        public const string NOTHING_OF_NOTHINGNESS = @"NothingOfNothingness";

        // s 後編追加
        // [動]
        public const string STRAIGHT_SMASH_JP = @"ストレート・スマッシュ";
        public const string DOUBLE_SLASH_JP = @"ダブル・スラッシュ";
        public const string CRUSHING_BLOW_JP = @"クラッシング・ブロー";
        public const string SOUL_INFINITY_JP = @"ソウル・インフィニティ";
        // [静]
        public const string COUNTER_ATTACK_JP = @"カウンター・アタック";
        public const string PURE_PURIFICATION_JP = @"ピュア・ピュリファイケーション";
        public const string ANTI_STUN_JP = @"アンチ・スタン";
        public const string STANCE_OF_DEATH_JP = @"スタンス・オブ・デス";
        // [柔]
        public const string STANCE_OF_FLOW_JP = @"スタンス・オブ・フロー";
        public const string ENIGMA_SENSE_JP = @"エニグマ・センス";
        public const string SILENT_RUSH_JP = @"サイレント・ラッシュ";
        public const string OBORO_IMPACT_JP = @"朧・インパクト";
        // [剛]
        public const string STANCE_OF_STANDING_JP = @"スタンス・オブ・スタンディング";
        public const string INNER_INSPIRATION_JP = @"インナー・インスピレーション";
        public const string KINETIC_SMASH_JP = @"キネティック・スマッシュ";
        public const string CATASTROPHE_JP = @"カタストロフィ";
        // [心眼]
        public const string TRUTH_VISION_JP = @"トゥルス・ヴィジョン";
        public const string HIGH_EMOTIONALITY_JP = @"ハイ・エモーショナリティ";
        public const string STANCE_OF_EYES_JP = @"スタンス・オブ・アイズ";
        public const string PAINFUL_INSANITY_JP = @"ペインフル・インサニティ";
        // [無心]
        public const string NEGATE_JP = @"ニゲイト";
        public const string VOID_EXTRACTION_JP = @"ヴォイド・エクストラクション";
        public const string CARNAGE_RUSH_JP = @"カルネージ・ラッシュ";
        public const string NOTHING_OF_NOTHINGNESS_JP = @"ナッシング・オブ・ナッシングネス";
        // e 後編追加

        // s 後編追加
        // [動　静]（完全逆）
        public const string NEUTRAL_SMASH = @"NeutralSmash";
        public const string NEUTRAL_SMASH_JP = @"ニュートラル・スマッシュ";
        public const string STANCE_OF_DOUBLE = @"StanceOfDouble";
        public const string STANCE_OF_DOUBLE_JP = @"スタンス・オブ・ダブル";

        // [動　柔]
        public const string SWIFT_STEP = @"SwiftStep";
        public const string SWIFT_STEP_JP = @"スウィフト・ステップ";
        public const string VIGOR_SENSE = @"VigorSense";
        public const string VIGOR_SENSE_JP = @"ヴィゴー・センス";

        // [動　剛]
        public const string CIRCLE_SLASH = @"CircleSlash";
        public const string CIRCLE_SLASH_JP = @"サークル・スラッシュ";
        public const string RISING_AURA = @"RisingAura";
        public const string RISING_AURA_JP = @"ライジング・オーラ";

        // [動　心眼]
        public const string RUMBLE_SHOUT = @"RumbleShout";
        public const string RUMBLE_SHOUT_JP = @"ランブル・シャウト";
        public const string ONSLAUGHT_HIT = @"OnslaughtHit";
        public const string ONSLAUGHT_HIT_JP = @"オンスロート・ヒット";

        // [動　無心]
        public const string COLORLESS_MOVE = @"ColorlessMove";
        public const string COLORLESS_MOVE_JP = @"カラレス・ムーブ";
        public const string ASCENSION_AURA = @"AscensionAura";
        public const string ASCENSION_AURA_JP = @"アセンション・オーラ";

        // [静　柔]
        public const string FUTURE_VISION = @"FutureVision";
        public const string FUTURE_VISION_JP = @"フューチャー・ヴィジョン";
        public const string UNKNOWN_SHOCK = @"UnknownShock";
        public const string UNKNOWN_SHOCK_JP = @"アンノウン・ショック";

        // [静　剛]
        public const string REFLEX_SPIRIT = @"ReflexSpirit";
        public const string REFLEX_SPIRIT_JP = @"リフレックス・スピリット";
        public const string FATAL_BLOW = @"FatalBlow";
        public const string FATAL_BLOW_JP = @"フェイタル・ブロー";

        // [静　心眼]
        public const string SHARP_GLARE = @"SharpGlare";
        public const string SHARP_GLARE_JP = @"シャープ・グレア";
        public const string CONCUSSIVE_HIT = @"ConcussiveHit";
        public const string CONCUSSIVE_HIT_JP = @"コンカッシヴ・ヒット";

        // [静　無心]
        public const string TRUST_SILENCE = @"TrustSilence";
        public const string TRUST_SILENCE_JP = @"トラスト・サイレンス";
        public const string MIND_KILLING = @"MindKilling";
        public const string MIND_KILLING_JP = @"マインド・キリング";

        // [柔　剛]（完全逆）
        public const string SURPRISE_ATTACK = @"SurpriseAttack";
        public const string SURPRISE_ATTACK_JP = @"サプライズ・アタック";
        public const string STANCE_OF_MYSTIC = @"StanceOfMystic";
        public const string STANCE_OF_MYSTIC_JP = @"スタンス・オブ・ミスティック";

        // [柔　心眼]
        public const string PSYCHIC_WAVE = @"PsychicWave";
        public const string PSYCHIC_WAVE_JP = @"サイキック・ウェイヴ";
        public const string NOURISH_SENSE = @"NourishSense";
        public const string NOURISH_SENSE_JP = @"ノリッシュ・センス";

        // [柔　無心]
        public const string RECOVER = @"Recover";
        public const string RECOVER_JP = @"リカバー";
        public const string IMPULSE_HIT = @"ImpulseHit";
        public const string IMPULSE_HIT_JP = @"インパルス・ヒット";

        // [剛　心眼]
        public const string VIOLENT_SLASH = @"ViolentSlash";
        public const string VIOLENT_SLASH_JP = @"ヴァイオレント・スラッシュ";
        public const string ONE_AUTHORITY = @"OneAuthority";
        public const string ONE_AUTHORITY_JP = @"ワン・オーソリティ";

        // [剛　無心]
        public const string OUTER_INSPIRATION = @"OuterInspiration";
        public const string OUTER_INSPIRATION_JP = @"アウター・インスピレーション";
        public const string HARDEST_PARRY = @"HardestParry";
        public const string HARDEST_PARRY_JP = @"ハーデスト・パリィ";

        // [心眼　無心]（完全逆）
        public const string STANCE_OF_SUDDENNESS = @"StanceOfSuddenness";
        public const string STANCE_OF_SUDDENNESS_JP = @"スタンス・オブ・サドンネス";
        public const string SOUL_EXECUTION = @"SoulExecution";
        public const string SOUL_EXECUTION_JP = @"ソウル・エグゼキューション";

        // e 後編追加
        #endregion

        #region "「元核」名称"
        public const string ARCHETYPE_COMMAND = @"潜在奥義【元核】";
        public const string ARCHETYPE_COMMAND_EN = @"Archetype Command";

        // 英語記述は不要。常に同じ名称を表示する。
        public const string ARCHETYPE_EIN = @"SYUTYU-DANZETSU";
        public const string ARCHETYPE_RANA = @"JUNKAN-SEIYAKU";
        public const string ARCHETYPE_OL = @"ORA-ORA-ORAAA!";
        public const string ARCHETYPE_VERZE = @"SHINZITSU-HAKAI";
        public const string ARCHETYPE_EIN_JP = @"集中と断絶";
        public const string ARCHETYPE_RANA_JP = @"循環の誓約";
        public const string ARCHETYPE_OL_JP = @"オラオラオラァ！";
        public const string ARCHETYPE_VERZE_JP = @"真実の破壊";
        #endregion

        #region "アイテム・コマンド"
        // 装備品コマンド
        public const string ITEMCOMMAND_FELTUS = @"Feltus";
        public const string ITEMCOMMAND_JUZA_PHANTASMAL = @"JuzaPhantasmal";
        public const string ITEMCOMMAND_ETERNAL_FATE = @"EternalFate";
        public const string ITEMCOMMAND_LIGHT_SERVANT = @"LightServant";
        public const string ITEMCOMMAND_SHADOW_SERVANT = @"ShadowServant";
        public const string ITEMCOMMAND_MAZE_CUBE = @"MazeCube";
        public const string ITEMCOMMAND_ADIL_RING_BLUE_BURN = @"AdilBlueBurn";
        public const string ITEMCOMMAND_DETACHMENT_ORB = @"DetachmentOrb";
        public const string ITEMCOMMAND_DEVIL_SUMMONER_TOME = @"DevilSummonerTome";
        public const string ITEMCOMMAND_VOID_HYMNSONIA = @"VoidHymnsonia";
        // 消耗品コマンド
        public const string ITEMCOMMAND_GENSEI_TAIMA = @"GenseiTaima";
        public const string ITEMCOMMAND_SHINING_AETHER = @"ShiningAether";
        public const string ITEMCOMMAND_BLACK_ELIXIR = @"BlackElixir";
        public const string ITEMCOMMAND_ELEMENTAL_SEAL = @"ElementalSeal";
        public const string ITEMCOMMAND_COLORLESS_ANTIDOTE = @"ColorlessAntidote";
        #endregion

        #region "スタイル名称"
        public static string STYLE_SWORD_DANCER = "Sword-Dancer";
        public static string STYLE_THE_GRADIATOR = "The-Gradiator";
        public static string STYLE_FIRE_WALKER = "Fire-Walker";
        public static string STYLE_ICE_WALKER = "Ice-Walker";
        public static string STYLE_MYSTIC_ENHANCER = "Mystic-Enhancer";
        public static string STYLE_BRAVE_SEEKER = "Brave-Seeker";
        public static string STYLE_ORACLE_COMMANDER = "Oracle-Commander";
        public static string STYLE_THE_DEFENDER = "The-Defender";
        public static string STYLE_ROYAL_KNIGHT = "Royal-Knight";
        public static string STYLE_VOICE_CALLER = "Voice-Caller";
        public static string STYLE_SHADOW_ROUGE = "Shadow-Rouge";
        public static string STYLE_BIRD_RANGER = "Bird-Ranger";
        public static string STYLE_AERIAL_HUNTER = "Aerial-Hunter";
        public static string STYLE_MAGE_BREAKER = "Mage-Breaker";
        public static string STYLE_HIGH_PRIEST = "High-Priest";
        public static string STYLE_BATTLE_SAGE = "Battle-Sage";
        #endregion

        #region "魔法消費コスト"
        // [聖]
        public static int FRESH_HEAL_COST = 20;
        public static int PROTECTION_COST = 40;
        public static int HOLY_SHOCK_COST = 70;
        public static int SAINT_POWER_COST = 100;
        public static int GLORY_COST = 250;
        public static int RESURRECTION_COST = 400;
        public static int CELESTIAL_NOVA_COST = 850;
        // [闇]
        public static int DARK_BLAST_COST = 15;
        public static int SHADOW_PACT_COST = 40;
        public static int LIFE_TAP_COST = 60;
        public static int BLACK_CONTRACT_COST = 120;
        public static int DEVOURING_PLAGUE_COST = 200;
        public static int BLOODY_VENGEANCE_COST = 450;
        public static int DAMNATION_COST = 900;
        // [火]
        public static int FIRE_BALL_COST = 15;
        public static int FLAME_AURA_COST = 30;
        public static int HEAT_BOOST_COST = 55;
        public static int FLAME_STRIKE_COST = 90;
        public static int VOLCANIC_WAVE_COST = 200;
        public static int IMMORTAL_RAVE_COST = 450;
        public static int LAVA_ANNIHILATION_COST = 1200;
        // [水]
        public static int ICE_NEEDLE_COST = 15;
        public static int ABSORB_WATER_COST = 30;
        public static int CLEANSING_COST = 50;
        public static int FROZEN_LANCE_COST = 80;
        public static int MIRROR_IMAGE_COST = 160;
        public static int PROMISED_KNOWLEDGE_COST = 500;
        public static int ABSOLUTE_ZERO_COST = 1200;
        // [理]
        public static int WORD_OF_POWER_COST = 30;
        public static int GALE_WIND_COST = 80;
        public static int WORD_OF_LIFE_COST = 100;
        public static int WORD_OF_FORTUNE_COST = 250;
        public static int AETHER_DRIVE_COST = 450;
        public static int GENESIS_COST = 0;
        public static int ETERNAL_PRESENCE_COST = 1000;
        // [空]
        public static int DISPEL_MAGIC_COST = 50;
        public static int RISE_OF_IMAGE_COST = 80;
        public static int DEFLECTION_COST = 150;
        public static int TRANQUILITY_COST = 200;
        public static int ONE_IMMUNITY_COST = 550;
        public static int WHITE_OUT_COST = 800;
        public static int TIME_STOP_COST = 1500;
        #endregion

        // s 後編追加
        #region "複合魔法消費コスト"
        // [聖闇]（完全逆）
        public static int PSYCHIC_TRANCE_COST = 800;
        public static int BLIND_JUSTICE_COST = 800;
        public static int TRANSCENDENT_WISH_COST = 1800;

        // [聖火]
        public static int FLASH_BLAZE_COST = 250;
        public static int LIGHT_DETONATOR_COST = 650;
        public static int ASCENDANT_METEOR_COST = 0;

        // [聖水]
        public static int SKY_SHIELD_COST = 350;
        public static int SACRED_HEAL_COST = 700;
        public static int EVER_DROPLET_COST = 950;

        // [聖理]
        public static int HOLY_BREAKER_COST = 210;
        public static int EXALTED_FIELD_COST = 650;
        public static int HYMN_CONTRACT_COST = 900;

        // [聖空]
        public static int STAR_LIGHTNING_COST = 400;
        public static int ANGEL_BREATH_COST = 600;
        public static int ENDLESS_ANTHEM_COST = 900;

        // [闇火]
        public static int BLACK_FIRE_COST = 350;
        public static int BLAZING_FIELD_COST = 600;
        public static int DEMONIC_IGNITE_COST = 1200;

        // [闇水]
        public static int BLUE_BULLET_COST = 250;
        public static int DEEP_MIRROR_COST = 400;
        public static int DEATH_DENY_COST = 1000;

        // [闇理]
        public static int WORD_OF_MALICE_COST = 250;
        public static int ABYSS_EYE_COST = 600;
        public static int SIN_FORTUNE_COST = 850;

        // [闇空]
        public static int DARKEN_FIELD_COST = 450;
        public static int DOOM_BLADE_COST = 700;
        public static int ECLIPSE_END_COST = 1500;

        // [火水]（完全逆）
        public static int FROZEN_AURA_COST = 800;
        public static int CHILL_BURN_COST = 1200;
        public static int ZETA_EXPLOSION_COST = 2500;

        // [火理]
        public static int ENRAGE_BLAST_COST = 300;
        public static int PIERCING_FLAME_COST = 650;
        public static int SIGIL_OF_HOMURA_COST = 1200;

        // [火空]
        public static int IMMOLATE_COST = 300;
        public static int PHANTASMAL_WIND_COST = 600;
        public static int RED_DRAGON_WILL_COST = 1000;

        // [水理]
        public static int WORD_OF_ATTITUDE_COST = 350;
        public static int STATIC_BARRIER_COST = 600;
        public static int AUSTERITY_MATRIX_COST = 1500;

        // [水空]
        public static int VANISH_WAVE_COST = 450;
        public static int VORTEX_FIELD_COST = 700;
        public static int BLUE_DRAGON_WILL_COST = 1000;

        // [理空]（完全逆）
        public static int SEVENTH_MAGIC_COST = 750;
        public static int PARADOX_IMAGE_COST = 1200;
        public static int WARP_GATE_COST = 2000;
        #endregion
        // e 後編追加

        #region "スキル消費コスト"
        // [動]
        public static int STRAIGHT_SMASH_COST = 15;
        public static int DOUBLE_SLASH_COST = 20;
        public static int CRUSHING_BLOW_COST = 35;
        public static int SOUL_INFINITY_COST = 85;
        // [静]
        public static int COUNTER_ATTACK_COST = 10;
        public static int PURE_PURIFICATION_COST = 15;
        public static int ANTI_STUN_COST = 20;
        public static int STANCE_OF_DEATH_COST = 35;
        // [柔]
        public static int STANCE_OF_FLOW_COST = 5;
        public static int ENIGMA_SENSE_COST = 15;
        public static int SILENT_RUSH_COST = 40;
        public static int OBORO_IMPACT_COST = 90;
        // [剛]
        public static int STANCE_OF_STANDING_COST = 5;
        public static int INNER_INSPIRATION_COST = 0;
        public static int KINETIC_SMASH_COST = 55;
        public static int CATASTROPHE_COST = 0; // 全消費する意味で０としている。
        // [心眼]
        public static int TRUTH_VISION_COST = 20;
        public static int HIGH_EMOTIONALITY_COST = 25;
        public static int STANCE_OF_EYES_COST = 30;
        public static int PAINFUL_INSANITY_COST = 80;
        // [無心]
        public static int NEGATE_COST = 10;
        public static int VOID_EXTRACTION_COST = 25;
        public static int CARNAGE_RUSH_COST = 60;
        public static int NOTHING_OF_NOTHINGNESS_COST = 100;

        // s 後編追加
        #region "複合スキルコスト"
        // [動　静]（完全逆）
        public static int NEUTRAL_SMASH_COST = 0; // SkillCost = 0
        public static int STANCE_OF_DOUBLE_COST = 50;

        // [動　柔]
        public static int SWIFT_STEP_COST = 20;
        public static int VIGOR_SENSE_COST = 35;

        // [動　剛]
        public static int CIRCLE_SLASH_COST = 20;
        public static int RISING_AURA_COST = 30;

        // [動　心眼]
        public static int RUMBLE_SHOUT_COST = 5;
        public static int ONSLAUGHT_HIT_COST = 20;

        // [動　無心]
        public static int COLORLESS_MOVE_COST = 15;
        public static int ASCENSION_AURA_COST = 30;

        // [静　柔]
        public static int FUTURE_VISION_COST = 20;
        public static int UNKNOWN_SHOCK_COST = 30;

        // [静　剛]
        public static int REFLEX_SPIRIT_COST = 10;
        public static int FATAL_BLOW_COST = 35;

        // [静　心眼]
        public static int SHARP_GLARE_COST = 15;
        public static int CONCUSSIVE_HIT_COST = 20;

        // [静　無心]
        public static int TRUST_SILENCE_COST = 10;
        public static int MIND_KILLING_COST = 20;

        // [柔　剛]（完全逆）
        public static int SURPRISE_ATTACK_COST = 35;
        public static int STANCE_OF_MYSTIC_COST = 60;

        // [柔　心眼]
        public static int PSYCHIC_WAVE_COST = 10;
        public static int NOURISH_SENSE_COST = 25;

        // [柔　無心]
        public static int RECOVER_COST = 10;
        public static int IMPULSE_HIT_COST = 20;

        // [剛　心眼]
        public static int VIOLENT_SLASH_COST = 15;
        public static int ONE_AUTHORITY_COST = 30;

        // [剛　無心]
        public static int OUTER_INSPIRATION_COST = 10;
        public static int HARDEST_PARRY_COST = 20;

        // [心眼　無心]（完全逆）
        public static int STANCE_OF_SUDDENNESS_COST = 40;
        public static int SOUL_EXECUTION_COST = 105;
        #endregion
        // e 後編追加
        #endregion

        #region "「元核」消費コスト"
        public static int ARCHITECT_EIN_COST = 0;
        public static int ARCHITECT_RANA_COST = 0;
        public static int ARCHITECT_OL_COST = 0;
        public static int ARCHITECT_VERZE_COST = 0;
        #endregion

        #region "音楽データ名"
        public static string BaseMusicFolder = @"BGM\"; //Environment.CurrentDirectory + @"BGM\";
        public static string BGM01 = @"01_town_silently";
        public static int BGM01LoopBegin = 0;
        public static string BGM02 = @"14_I_Will_Go_Dungeon"; // @"02_dungeon_seeking";
        public static int BGM02LoopBegin = 0; // 106500;
        public static string BGM03 = @"03_battle_warning";
        public static int BGM03LoopBegin = 0;
        public static string BGM04 = @"04_The_Flame";
        public static int BGM04LoopBegin = 0;
        public static string BGM05 = @"05_Finally_Bystander";
        public static float BGM05LoopBegin = 33.7F;
        public static string BGM06 = @"19_Silent_Moving"; // @"06_Refuse";
        public static int BGM06LoopBegin = 0;
        public static string BGM07 = @"07_Systematic_Dominance";
        public static int BGM07LoopBegin = 0;
        public static string BGM08 = @"02_dungeon_seeking";
        public static int BGM08LoopBegin = 0;
        //public static string BGM09 = @"09_Sea_Ground_Sky";
        //public static int BGM09LoopBegin = 0;
        public static string BGM10 = @"10_WindOfVerze";
        public static int BGM10LoopBegin = 0;
        public static string BGM11 = @"11_DUEL_FACE_AND_FACE";
        public static int BGM11LoopBegin = 0;
        public static string BGM12 = @"12_Opening_Live_and_Space";
        public static int BGM12LoopBegin = 0;
        public static string BGM13 = @"13_Entrance_And_Walls";
        public static int BGM13LoopBegin = 0;
        public static string BGM14 = @"14_I_Will_Go_Dungeon";
        public static int BGM14LoopBegin = 0;
        public static string BGM15 = @"15_The_Ear_Ring_Remember";
        public static int BGM15LoopBegin = 0;
        public static string BGM16 = @"16_Wanna_Understand";
        public static int BGM16LoopBegin = 0;
        //public static string BGM17 = @"17_wake_up_and_fight";
        //public static int BGM17LoopBegin = 0;
        public static string BGM18 = @"18_MatrixDragon";
        public static int BGM18LoopBegin = 0;
        public static string BGM19 = @"19_Silent_Moving";
        public static int BGM19LoopBegin = 0;
        //public static string BGM20 = @"20_TimeEnd_StartReason";
        //public static int BGM20LoopBegin = 0;
        public static string BGM21 = @"21_The_Best_Battle";
        public static int BGM21LoopBegin = 0;
        //public static string BGM22 = @"22_TimeEnd_FanFare";
        //public static int BGM22LoopBegin = 0;
        public static string BGM23 = @"23_Verze_StartReason";
        public static int BGM23LoopBegin = 0;
        #endregion

        #region "環境ファイル系"
        public static string BaseSoundFolder = @"Sounds/";
        public static string BaseSaveFolder = Environment.CurrentDirectory + @"\Save\";
        public static string BaseResourceFolder = @""; // Environment.CurrentDirectory + @"\Resource\"; change unity

        public static string[] FloorFolder = { @"Floor1\", @"Floor2\", @"Floor3\", @"Floor4\", @"Floor5\" };

        public static string GameSettingFileName = Environment.CurrentDirectory + @"\" + @"GameSetting.xml";

        public static string WorldSaveNum = "999_";
        public static int WORLD_SAVE_NUM = 999;
        public static string AutoSaveNum = "210_";
        public static int AUTOSAVE_PAGE_NUM = 21;
        public static string SAVELOAD_NEW = "SaveLoadNew2";
        public static string SAVELOAD_NEW_AUTO = "SaveLoadNewAuto";
        #endregion

        #region "戦闘、敵のBuffUp汎用"
        public const string BUFF_PHYSICAL_ATTACK_UP = @"BuffPhysicalAttackUp";
        public const string BUFF_PHYSICAL_ATTACK_DOWN = @"BuffPhysicalAttackDown";

        public const string BUFF_PHYSICAL_DEFENSE_UP = @"BuffPhysicalDefenseUp";
        public const string BUFF_PHYSICAL_DEFENSE_DOWN = @"BuffPhysicalDefenseDown";

        public const string BUFF_MAGIC_ATTACK_UP = @"BuffMagicAttackUp";
        public const string BUFF_MAGIC_ATTACK_DOWN = @"BuffMagicAttackDown";

        public const string BUFF_MAGIC_DEFENSE_UP = @"BuffMagicDefenseUp";
        public const string BUFF_MAGIC_DEFENSE_DOWN = @"BuffMagicDefenseDown";

        public const string BUFF_SPEED_UP = @"BuffSpeedUp";
        public const string BUFF_SPEED_DOWN = @"BuffSpeedDown";

        public const string BUFF_REACTION_UP = @"BuffReactionUp";
        public const string BUFF_REACTION_DOWN = @"BuffReactionDown";

        public const string BUFF_POTENTIAL_UP = @"BuffPotentialUp";
        public const string BUFF_POTENTIAL_DOWN = @"BuffPotentialDown";

        public const string BUFF_STRENGTH_UP = @"BuffStrengthUp";
        public const string BUFF_AGILITY_UP = @"BuffAgilityUp";
        public const string BUFF_INTELLIGENCE_UP = @"BuffIntelligenceUp";
        public const string BUFF_STAMINA_UP = @"BuffStaminaUp";
        public const string BUFF_MIND_UP = @"BuffMindUp";

        public const string BUFF_LIGHT_UP = @"BuffLightUp";
        public const string BUFF_LIGHT_DOWN = @"BuffLightDown";

        public const string BUFF_SHADOW_UP = @"BuffShadowUp";
        public const string BUFF_SHADOW_DOWN = @"BuffShadowDown";

        public const string BUFF_FIRE_UP = @"BuffFireUp";
        public const string BUFF_FIRE_DOWN = @"BuffFireDown";

        public const string BUFF_ICE_UP = @"BuffIceUp";
        public const string BUFF_ICE_DOWN = @"BuffIcedown";

        public const string BUFF_FORCE_UP = @"BuffForceUp";
        public const string BUFF_FORCE_DOWN = @"BuffForceDown";

        public const string BUFF_WILL_UP = @"BuffWillUp";
        public const string BUFF_WILL_DOWN = @"BuffWillDown";
        #endregion

        #region "素材ファイル集"
        public const string JUNK_FIREDAMAGE = @"FireDamage";
        #endregion

        public static int MAINPLAYER_FIRST_STRENGTH = 5;
        public static int MAINPLAYER_FIRST_AGILITY = 3;
        public static int MAINPLAYER_FIRST_INTELLIGENCE = 2;
        public static int MAINPLAYER_FIRST_STAMINA = 4;
        public static int MAINPLAYER_FIRST_MIND = 3;

        public static int SECONDPLAYER_FIRST_STRENGTH = 2;
        public static int SECONDPLAYER_FIRST_AGILITY = 3;
        public static int SECONDPLAYER_FIRST_INTELLIGENCE = 5;
        public static int SECONDPLAYER_FIRST_STAMINA = 3;
        public static int SECONDPLAYER_FIRST_MIND = 4;
        public static int SECONDPLAYER_END_STRENGTH = 950;
        public static int SECONDPLAYER_END_AGILITY = 1200;
        public static int SECONDPLAYER_END_INTELLIGENCE = 900;
        public static int SECONDPLAYER_END_STAMINA = 700;
        public static int SECONDPLAYER_END_MIND = 325;

        public static int THIRDPLAYER_FIRST_STRENGTH = 4;
        public static int THIRDPLAYER_FIRST_AGILITY = 5;
        public static int THIRDPLAYER_FIRST_INTELLIGENCE = 4;
        public static int THIRDPLAYER_FIRST_STAMINA = 3;
        public static int THIRDPLAYER_FIRST_MIND = 1;

        // s 後編追加
        public static int OL_LANDIS_FIRST_STRENGTH = 200; // 5;
        public static int OL_LANDIS_FIRST_AGILITY = 116; // 3;
        public static int OL_LANDIS_FIRST_INTELLIGENCE = 118; // 4;
        public static int OL_LANDIS_FIRST_STAMINA = 26; // 1;
        public static int OL_LANDIS_FIRST_MIND = 40; // 4;

        public static int OL_LANDIS_STRENGTH_2 = 2100;
        public static int OL_LANDIS_AGILITY_2 = 800;
        public static int OL_LANDIS_INTELLIGENCE_2 = 5;
        public static int OL_LANDIS_STAMINA_2 = 599;
        public static int OL_LANDIS_MIND_2 = 350;

        public static int VERZE_ARTIE_SECOND_STRENGTH = 10;
        public static int VERZE_ARTIE_SECOND_AGILITY = 300;
        public static int VERZE_ARTIE_SECOND_INTELLIGENCE = 120;
        public static int VERZE_ARTIE_SECOND_STAMINA = 65;
        public static int VERZE_ARTIE_SECOND_MIND = 5;

        public static int VERZE_ARTIE_STRENGTH_3 = 10;
        public static int VERZE_ARTIE_AGILITY_3 = 2600;
        public static int VERZE_ARTIE_INTELLIGENCE_3 = 120;
        public static int VERZE_ARTIE_STAMINA_3 = 1000;
        public static int VERZE_ARTIE_MIND_3 = 124;

        public static int SINIKIA_KAHLHANTZ_STRENGTH_2 = 5;
        public static int SINIKIA_KAHLHANTZ_AGILITY_2 = 600;
        public static int SINIKIA_KAHLHANTZ_INTELLIGENCE_2 = 2200;
        public static int SINIKIA_KAHLHANTZ_STAMINA_2 = 999;
        public static int SINIKIA_KAHLHANTZ_MIND_2 = 50;
        // e 後編追加

        public static string TEXT_VIGILANCE_MODE = @"警戒モード";
        public static string TEXT_FINDENEMY_MODE = @"索敵モード";
        public static string VIGILANCE_MODE_RESOURCE = @"VigilanceMode";
        public static string FINDENEMY_MODE_RESOURCE = @"PathfindingMode";

        public static string InstallComponentError = @"ご迷惑おかけしております。SlimDXコンポーネント、または、DirectXコンポーネントの初期化に失敗しました。\r\nBGMと効果音をオフにします。\r\n大変お手数ですが、お使いのＰＣ環境を確認してください。";
        public static string BattleRoutineError = @"戦闘中にエラーが発生しました。大変ご迷惑おかけしております。お手数ですが、再起動してやり直してください。";

        #region "後編用に新しく作成されるデータ集"
        public static int MAX_PARTY_MEMBER = 3;

        public static int SOUL_ATTRIBUTE_NUM = 8;
        public static int MAX_SOUL_ATTRIBUTE = 99;

        public static int CHARACTER_MAX_LEVEL1 = 20;
        public static int CHARACTER_MAX_LEVEL2 = 35; // 後編２階まで
        public static int CHARACTER_MAX_LEVEL3 = 50; // 後編３階まで
        public static int CHARACTER_MAX_LEVEL4 = 60; // 後編４階まで
        public static int CHARACTER_MAX_LEVEL5 = 65; // 後編５階まで
        public static int CHARACTER_MAX_LEVEL6 = 70; // 後編真実世界まで

        public static int SPELL_TYPE_NUM = 6;
        public static int SKILL_TYPE_NUM = 6;
        public static int SPELL_ONETYPE_MAX_NUM = 7;
        public static int SKILL_ONETYPE_MAX_NUM = 4;
        public static int SPELL_MAX_NUM = 42;
        public static int SKILL_MAX_NUM = 24;
        public static int BATTLE_COMMAND_MAX = 9;

        public static int MAX_PARAMETER = 10000; // パラメタ最大値（最後のボス仕様に影響があります）
        public static int TRUTH_FIRST_POS = DUNGEON_MOVE_LEN * 13 + 39; // 後編ダンジョン開始位置
        public static int TRUTH_DUNGEON_COLUMN = 60; // 後編ダンジョンの列数
        public static int TRUTH_DUNGEON_ROW = 40; // 後編ダンジョンの行数
        public static int CAMERA_BORDER_X_LEFT = 0;//4;
        public static int CAMERA_BORDER_X_RIGHT = 60;//52;
        public static int CAMERA_BORDER_Y_TOP = 0;//-3;
        public static int CAMERA_BORDER_Y_BOTTOM = -40;//-36;
        public static int CAMERA_WORLD_POINT_X = 0;//5;
        public static int CAMERA_WORLD_POINT_Y = 0;//-3;

        public static int BUFFPANEL_OFFSET_X = 5; // add unity
        public static int BUFFPANEL_BUFF_WIDTH = 40; // -25; change unity
        public static int BUFF_NUM = 180; // [警告] component数が200*6=1200, 1000だと6000でスローダウン現象につながる
        public static int BUFF_SIZE_X = 25;
        public static int BUFF_SIZE_Y = 40;
        public static int INFINITY = 99999;

        public static int TILE_AREA_NUM = 4;
        public static string PLAYER_MARK = @"Player";
        public static string TREASURE_BOX = @"TreasureBox";
        public static string TREASURE_BOX_OPEN = @"TreasureBoxOpen";
        public static string BOARD = @"Board";
        public static string UPSTAIR = @"Upstair";
        public static string DOWNSTAIR = @"Downstair";
        public static string BLUE_WALL_T = @"BlueWall-T";
        public static string BLUE_WALL_L = @"BlueWall-L";
        public static string BLUE_WALL_R = @"BlueWall-R";
        public static string BLUE_WALL_B = @"BlueWall-B";
        public static string MIRROR = @"Mirror";
        public static string BLUEORB = @"BlueOrb";
        public static string FOUNTAIN = @"Fountain";
        public static string MARK_ENEMY = @"Enemy";


        #region "一般コマンド名称"
        public const string ATTACK_JP = @"攻撃";
        public const string ATTACK_EN = @"Attack";
        public const string DEFENSE_JP = @"防御";
        public const string DEFENSE_EN = @"Defense";
        public const string STAY_JP = @"待機";
        public const string STAY_EN = @"Stay";
        public const string WEAPON_SPECIAL_JP = @"武器能力（メイン）";
        public const string WEAPON_SPECIAL_EN = @"Weapon Special Main";
        public const string WEAPON_SPECIAL_LEFT_JP = @"武器能力（サブ）";
        public const string WEAPON_SPECIAL_LEFT_EN = @"Weapon Special Sub";
        public const string TAMERU_JP = @"ためる";
        public const string TAMERU_EN = @"Power Charge";
        public const string ACCESSORY_SPECIAL_JP = @"アクセサリ【１】";
        public const string ACCESSORY_SPECIAL_EN = @"Accessory 1";
        public const string ACCESSORY_SPECIAL2_JP = @"アクセサリ【２】";
        public const string ACCESSORY_SPECIAL2_EN = @"Accessory 2";

        public const string TOSSIN = @"突進";
        public const string BUFFUP_STRENGTH = @"パワーアップ【力】";
        public const string MAGIC_ATTACK = @"魔法攻撃";
        public const string RENZOKU_ATTACK = @"連続攻撃";

        // 戦闘中のアニメーション文字
        public const string SUCCESS_DEFLECTION = @"物理反射！";
        public const string SUCCESS_AVOID = @"回避！";

        public const string MISS = @"ミス！";
        public const string MISS_ACTION = @"行動ミス！";
        public const string MISS_SPELL = @"詠唱ミス！";
        public const string MISS_SKILL = @"スキル失敗！";
        public const string FAIL_COUNTER = @"カウンターミス！";
        public const string FAIL_DEFLECTION = @"物理反射ミス！";
        public const string SUCCESS_COUNTER = @"カウンター発動！";
        public const string RESIST_DISPEL = @"ディスペル耐性！";

        // 負
        public const string EFFECT_PRESTUNNING = @"恐怖";
        public const string EFFECT_STUN = @"スタン";
        public const string EFFECT_SILENCE = @"沈黙";
        public const string EFFECT_POISON = @"猛毒";
        public const string EFFECT_TEMPTATION = @"誘惑";
        public const string EFFECT_FROZEN = @"凍結";
        public const string EFFECT_PARALYZE = @"麻痺";
        public const string EFFECT_SLOW = @"鈍化";
        public const string EFFECT_BLIND = @"暗闇";
        public const string EFFECT_SLIP = @"スリップ";
        public const string EFFECT_NORESURRECTION = @"蘇生不可";
        public const string EFFECT_NOGAIN_LIFE = @"ライフ回復不可";

        public const string EFFECT_CANNOT_BUFF = @"封殺";

        // 正
        public const string EFFECT_BLINDED = @"退避状態";

        public const string RESIST_FEAR = @"恐怖耐性！";
        public const string RESIST_STUN = @"スタン耐性！";
        public const string RESIST_SILENCE = @"沈黙耐性！";
        public const string RESIST_POISON = @"猛毒耐性！";
        public const string RESIST_TEMPTATION = @"誘惑耐性！";
        public const string RESIST_FROZEN = @"凍結耐性！";
        public const string RESIST_PARALYZE = @"麻痺耐性！";
        public const string RESIST_SLOW = @"鈍化耐性！";
        public const string RESIST_BLIND = @"暗闇耐性！";
        public const string RESIST_SLIP = @"スリップ耐性！";
        public const string RESIST_NORESURRECTION = @"蘇生不可耐性！";

        public const string RESIST_LIFE_DOWN = @"ライフダウン耐性！";

        public const string RESIST_LIGHT_UP = @"聖耐性付与";
        public const string RESIST_SHADOW_UP = @"闇耐性付与";
        public const string RESIST_FIRE_UP = @"火耐性付与";
        public const string RESIST_ICE_UP = @"水耐性付与";
        public const string RESIST_FORCE_UP = @"理耐性付与";
        public const string RESIST_WILL_UP = @"空耐性付与";

        public const string BUFF_DANZAI_KAGO = @"断罪の加護";
        public const string BUFF_FIREDAMAGE2 = @"炎ダメージ";
        public const string BUFF_BLACK_MAGIC = @"魔法2回詠唱";
        public const string BUFF_CHAOS_DESPERATE = @"死亡予告";

        public const string BUFF_FELTUS = @"【神】の蓄積";
        public const string BUFF_JUZA_PHANTASMAL = @"【颯】の蓄積";
        public const string BUFF_ETERNAL_FATE_RING = @"【轟】の蓄積";
        public const string BUFF_LIGHT_SERVANT = @"【聖】の蓄積";
        public const string BUFF_SHADOW_SERVANT = @"【闇】の蓄積";
        public const string BUFF_ADIL_BLUE_BURN = @"【蒼】の蓄積";
        public const string BUFF_MAZE_CUBE = @"【迷】の蓄積";
        public const string BUFF_SHADOW_BIBLE = @"【死亡時に蘇生】";
        public const string BUFF_DETACHMENT_ORB = @"【全ダメージ無効】";
        public const string BUFF_DEVIL_SUMMONER_TOME = @"【悪魔召喚中】";
        public const string BUFF_VOID_HYMNSONIA = @"【呪い】心 -1000";

        public const string BUFF_SAGE_POTION_MINI = @"全耐性付与";
        public const string BUFF_GENSEI_TAIMA = @"【即死】回避／死亡時ライフ半分回復";
        public const string BUFF_SHINING_AETHER = @"元核スキル発動可／全ダメージ無効";
        public const string BUFF_BLACK_ELIXIR = @"最大ライフ増強／ライフダウン耐性";
        public const string BUFF_ELEMENTAL_SEAL = @"負の影響耐性";
        public const string BUFF_COLORLESS_ANTIDOTE = @"デバフ耐性";

        public const string BUFF_ICHINARU_HOMURA = @"毎ターン、焔ダメージ";
        public const string BUFF_ABYSS_FIRE = @"物理攻撃か魔法攻撃を行う度に、アビスダメージ";
        public const string BUFF_LIGHT_AND_SHADOW = @"物理／魔法ダメージ０";
        public const string BUFF_ETERNAL_DROPLET = @"ライフ／マナ回復";
        public const string BUFF_AUSTERITY_MATRIX_OMEGA = @"プラスBUFF無効";
        public const string BUFF_VOICE_OF_ABYSS = @"ライフ回復０";
        public const string BUFF_ABYSS_WILL = @"焔／アビスダメージ上昇";
        public const string BUFF_THE_ABYSS_WALL = @"ダメージ吸収バリア";

        public const string BUFF_TIME_SEQUENCE_1 = @"時間律【憎業】";
        public const string BUFF_TIME_SEQUENCE_2 = @"時間律【零空】";
        public const string BUFF_TIME_SEQUENCE_3 = @"時間律【盛栄】";
        public const string BUFF_TIME_SEQUENCE_4 = @"時間律【絶剣】";
        public const string BUFF_TIME_SEQUENCE_5 = @"時間律【緑永】";
        public const string BUFF_TIME_SEQUENCE_6 = @"完全絶対時間律【終焉】";

        public const string PHYSICAL_ATTACK_UP = @"物理攻撃UP";
        public const string PHYSICAL_ATTACK_DOWN = @"物理攻撃DOWN";
        public const string PHYSICAL_DEFENSE_UP = @"物理防御UP";
        public const string PHYSICAL_DEFENSE_DOWN = @"物理防御DOWN";
        public const string MAGIC_ATTACK_UP = @"魔法攻撃UP";
        public const string MAGIC_ATTACK_DOWN = @"魔法攻撃DOWN";
        public const string MAGIC_DEFENSE_UP = @"魔法耐性UP";
        public const string MAGIC_DEFENSE_DOWN = @"魔法耐性DOWN";
        public const string BATTLE_SPEED_UP = @"戦闘速度UP";
        public const string BATTLE_SPEED_DOWN = @"戦闘速度DOWN";
        public const string BATTLE_REACTION_UP = @"戦闘反応UP";
        public const string BATTLE_REACTION_DOWN = @"戦闘反応DOWN";
        public const string POTENTIAL_UP = @"潜在能力UP";
        public const string POTENTIAL_DOWN = @"潜在能力DOWN";

        public const string EFFECT_STRENGTH_UP = @"【力】UP";
        public const string EFFECT_AGILITY_UP = @"【技】UP";
        public const string EFFECT_INTELLIGENCE_UP = @"【知】UP";
        public const string EFFECT_STAMINA_UP = @"【体】UP";
        public const string EFFECT_MIND_UP = @"【心】UP";

        public const string IMMUNE_DAMAGE = @"ダメージ無効！";

        public const string BROKEN_ITEM = @"アイテム破損";

        public const string BUFF_EFFECT = @"BUFF効果";
        public const string ITEM_EFFECT = @"ITEM効果";
        #endregion

        #region "ダンジョンタイルデータ名"
        public const string TILEINFO_1 = @"Downstair";
        public const string TILEINFO_2 = @"Downstair-WallLRB";
        public const string TILEINFO_3 = @"Downstair-WallT";
        public const string TILEINFO_4 = @"Downstair-WallTLB";
        public const string TILEINFO_5 = @"Downstair-WallTRB";
        public const string TILEINFO_6 = @"Upstair-WallLRB";
        public const string TILEINFO_7 = @"Upstair-WallRB";
        public const string TILEINFO_8 = @"Upstair-WallTLR";
        public const string TILEINFO_9 = @"TreasureBox";
        public const string TILEINFO_10 = @"UnknownTile";
        public const string TILEINFO_10_2 = @"UnknownTile_Check";
        public const string TILEINFO_11 = @"Upstair";
        public const string TILEINFO_12 = @"Board";

        public const string TILEINFO_13 = @"Tile1";
        public const string TILEINFO_14 = @"Tile1-WallB";
        public const string TILEINFO_15 = @"Tile1-WallB-DummyB";
        public const string TILEINFO_16 = @"Tile1-WallL";
        public const string TILEINFO_17 = @"Tile1-WallLB";
        public const string TILEINFO_18 = @"Tile1-WallLR";
        public const string TILEINFO_19 = @"Tile1-WallLRB";
        public const string TILEINFO_20 = @"Tile1-WallLR-DummyL";
        public const string TILEINFO_21 = @"Tile1-WallR";
        public const string TILEINFO_22 = @"Tile1-WallRB";
        public const string TILEINFO_23 = @"Tile1-WallR-DummyR";
        public const string TILEINFO_24 = @"Tile1-WallT";
        public const string TILEINFO_25 = @"Tile1-WallTB";
        public const string TILEINFO_26 = @"Tile1-WallTL";
        public const string TILEINFO_27 = @"Tile1-WallTLB";
        public const string TILEINFO_28 = @"Tile1-WallTLR";
        public const string TILEINFO_29 = @"Tile1-WallTLRB";
        public const string TILEINFO_30 = @"Tile1-WallTR";
        public const string TILEINFO_31 = @"Tile1-WallTRB";

        public const string TILEINFO_32 = @"Tile1-WallT-NumT1";
        public const string TILEINFO_33 = @"Tile1-WallL-NumT2";
        public const string TILEINFO_34 = @"Tile1-NumT3";
        public const string TILEINFO_35 = @"Tile1-WallLB-NumT4";
        public const string TILEINFO_36 = @"Tile1-Wall-NumT5";
        public const string TILEINFO_37 = @"Tile1-WallRB-NumT6";
        public const string TILEINFO_38 = @"Tile1-WallT-NumT7";
        public const string TILEINFO_39 = @"Tile1-Wall-NumT8";

        public const string TILEINFO_40 = @"Tile1-Blue";
        public const string TILEINFO_41 = @"Tile1-Black";

        public const string TILEINFO_42 = @"Tile1-WallRB-DummyB";

        public const string TILEINFO_43 = @"Mirror";

        public const string TILEINFO_44 = @"BlueOrb";
        public const string TILEINFO_45 = @"Enemy";

        public const string TILEINFO_46 = @"Tile1-Normal";
        public const string TILEINFO_47 = @"Tile1-Normal-WallT";
        public const string TILEINFO_48 = @"Tile1-Normal-WallL";
        public const string TILEINFO_49 = @"Tile1-Normal-WallR";
        public const string TILEINFO_50 = @"Tile1-Normal-WallB";
        public const string TILEINFO_51 = @"Tile1-Normal-NearT";
        public const string TILEINFO_52 = @"Tile1-Normal-NearL";
        public const string TILEINFO_53 = @"Tile1-Normal-NearR";
        public const string TILEINFO_54 = @"Tile1-Normal-NearB";
        public const string TILEINFO_55 = @"Tile1-Normal-NearTL";
        public const string TILEINFO_56 = @"Tile1-Normal-NearTR";
        public const string TILEINFO_57 = @"Tile1-Normal-NearLB";
        public const string TILEINFO_58 = @"Tile1-Normal-NearRB";
        public const string TILEINFO_59 = @"Tile1-Normal-NearT-EdgeLB";
        public const string TILEINFO_60 = @"Tile1-Normal-NearT-EdgeRB";
        public const string TILEINFO_61 = @"Tile1-Normal-NearL-EdgeTR";
        public const string TILEINFO_62 = @"Tile1-Normal-NearL-EdgeRB";
        public const string TILEINFO_63 = @"Tile1-Normal-NearR-EdgeTL";
        public const string TILEINFO_64 = @"Tile1-Normal-NearR-EdgeLB";
        public const string TILEINFO_65 = @"Tile1-Normal-NearB-EdgeTL";
        public const string TILEINFO_66 = @"Tile1-Normal-NearB-EdgeTR";
        public const string TILEINFO_67 = @"Normal-3Edge_TL";
        public const string TILEINFO_68 = @"Normal-3Edge_TR";
        public const string TILEINFO_69 = @"Normal-3Edge_LB";
        public const string TILEINFO_70 = @"Normal-3Edge_RB";
        public const string TILEINFO_71 = @"Normal-1EdgeTL";
        public const string TILEINFO_72 = @"Normal-1EdgeTR";
        public const string TILEINFO_73 = @"Normal-1EdgeLB";
        public const string TILEINFO_74 = @"Normal-1EdgeRB";
        #endregion

        #region "アイテム名称"
        #region "初期"
        // アイテム名称を各ソースコードに記述すると誤りが発生するため、こちらで記載します。
        public const string RARE_TOOMI_BLUE_SUISYOU = @"遠見の青水晶";
        public const string RARE_EARRING_OF_LANA = @"ラナのイヤリング";
        public const string POOR_PRACTICE_SWORD = @"練習用の剣";
        public const string POOR_PRACTICE_SHILED = @"練習用の盾";
        public const string POOR_COTE_OF_PLATE = @"コート・オブ・プレート";
        public const string POOR_PRACTICE_KNUCKLE = @"練習用のナックル";
        public const string POOR_LIGHT_CROSS = @"軽めの舞踏衣";
        public const string COMMON_SANGO_BRESLET = @"珊瑚のブレスレット";
        #endregion

        #region "１階"
        public const string POOR_HINJAKU_ARMRING = @"貧弱な腕輪";
        public const string POOR_USUYOGORETA_FEATHER = @"薄汚れた付け羽";
        public const string POOR_NON_BRIGHT_ORB = @"輝きの無いオーブ";
        public const string POOR_KUKEI_BANGLE = @"矩形のバングル";
        public const string POOR_SUTERARESHI_EMBLEM = @"捨てられし紋章";
        public const string POOR_ARIFURETA_STATUE = @"ありふれた彫像";
        public const string POOR_NON_ADJUST_BELT = @"調整できないベルト";
        public const string POOR_SIMPLE_EARRING = @"シンプルなイヤリング";
        public const string POOR_KATAKUZURESHITA_FINGERRING = @"型崩れした指輪";
        public const string POOR_IROASETA_CHOKER = @"色褪せたチョーカー";
        public const string POOR_YOREYORE_MANTLE = @"よれよれのマント";
        public const string POOR_NON_HINSEI_CROWN = @"品性のない王冠";
        public const string POOR_TUKAIFURUSARETA_SWORD = @"使い古された剣";
        public const string POOR_TUKAINIKUI_LONGSWORD = @"使いにくい長剣";
        public const string POOR_GATAGAKITERU_ARMOR = @"ガタがきてる鎧";
        public const string POOR_FESTERING_ARMOR = @"フェスタリング・アーマー";
        public const string POOR_HINSO_SHIELD = @"貧粗な盾";
        public const string POOR_MUDANIOOKII_SHIELD = @"無駄に大きい盾";

        public const string COMMON_RED_PENDANT = @"レッド・ペンダント";
        public const string COMMON_BLUE_PENDANT = @"ブルー・ペンダント";
        public const string COMMON_PURPLE_PENDANT = @"パープル・ペンダント";
        public const string COMMON_GREEN_PENDANT = @"グリーン・ペンダント";
        public const string COMMON_YELLOW_PENDANT = @"イエロー・ペンダント";
        public const string COMMON_SISSO_ARMRING = @"質素な腕輪";
        public const string COMMON_FINE_FEATHER = @"ファイン・フェザー";
        public const string COMMON_KIREINA_ORB = @"綺麗なオーブ";
        public const string COMMON_FIT_BANGLE = @"フィット・バングル";
        public const string COMMON_PRISM_EMBLEM = @"プリズム・エムブレム";
        public const string COMMON_FINE_SWORD = @"ファイン・ソード";
        public const string COMMON_TWEI_SWORD = @"ツヴァイ・ソード";
        public const string COMMON_FINE_ARMOR = @"ファイン・アーマー";
        public const string COMMON_GOTHIC_PLATE = @"ゴシック・プレート";
        public const string COMMON_FINE_SHIELD = @"ファイン・シールド";
        public const string COMMON_GRIPPING_SHIELD = @"グリッピング・シールド";

        public const string RARE_JOUSITU_BLUE_POWERRING = @"上質の青いパワーリング";
        public const string RARE_KOUJOUSINYADORU_RED_ORB = @"向上心の宿る赤いオーブ";
        public const string RARE_MAGICIANS_MANTLE = @"マジシャンズ・マント";
        public const string RARE_BEATRUSH_BANGLE = @"ビートラッシュ・バングル";
        public const string RARE_AERO_BLADE = @"エアロ・ブレード";

        public const string POOR_OLD_USELESS_ROD = @"古ぼけた杖";
        public const string POOR_KISSAKI_MARUI_TUME = @"切先が丸い爪";
        public const string POOR_BATTLE_HUMUKI_BUTOUGI = @"戦闘に不向きな舞踏衣";
        public const string POOR_SIZE_AWANAI_ROBE = @"サイズが合わないローブ";
        public const string POOR_NO_CONCEPT_RING = @"特徴が失せている腕輪";
        public const string POOR_HIGHCOLOR_MANTLE = @"ハイカラなマント";
        public const string POOR_EIGHT_PENDANT = @"八角ペンダント";
        public const string POOR_GOJASU_BELT = @"ゴージャスベルト";
        public const string POOR_EGARA_HUMEI_EMBLEM = @"絵柄不明のペンダント";
        public const string POOR_HAYATOTIRI_ORB = @"はやとちりなオーブ";

        public const string COMMON_COPPER_RING_TORA = @"銅の腕輪『虎』";
        public const string COMMON_COPPER_RING_IRUKA = @"銅の腕輪『イルカ』";
        public const string COMMON_COPPER_RING_UMA = @"銅の腕輪『馬』";
        public const string COMMON_COPPER_RING_KUMA = @"銅の腕輪『熊』";
        public const string COMMON_COPPER_RING_HAYABUSA = @"銅の腕輪『隼』";
        public const string COMMON_COPPER_RING_TAKO = @"銅の腕輪『タコ』";
        public const string COMMON_COPPER_RING_USAGI = @"銅の腕輪『兎』";
        public const string COMMON_COPPER_RING_KUMO = @"銅の腕輪『蜘蛛』";
        public const string COMMON_COPPER_RING_SHIKA = @"銅の腕輪『鹿』";
        public const string COMMON_COPPER_RING_ZOU = @"銅の腕輪『象』";
        public const string COMMON_RED_AMULET = @"アミュレット『赤』";
        public const string COMMON_BLUE_AMULET = @"アミュレット『青』";
        public const string COMMON_PURPLE_AMULET = @"アミュレット『紫』";
        public const string COMMON_GREEN_AMULET = @"アミュレット『緑』";
        public const string COMMON_YELLOW_AMULET = @"アミュレット『黄』";
        public const string COMMON_SHARP_CLAW = @"シャープ・クロー";
        public const string COMMON_LIGHT_CLAW = @"ライト・クロー";
        public const string COMMON_WOOD_ROD = @"ウッド・ロッド";
        public const string COMMON_SHORT_SWORD = @"ショート・ソード";
        public const string COMMON_BASTARD_SWORD = @"バスタード・ソード";
        public const string COMMON_LETHER_CLOTHING = @"レザー・クロス";
        public const string COMMON_COTTON_ROBE = @"コットン・ローブ";
        public const string COMMON_COPPER_ARMOR = @"銅の鎧";
        public const string COMMON_HEAVY_ARMOR = @"ヘヴィ・アーマー";
        public const string COMMON_IRON_SHIELD = @"アイアン・シールド";

        public const string RARE_SINTYUU_RING_KUROHEBI = @"真鍮の腕輪『黒蛇』";
        public const string RARE_SINTYUU_RING_HAKUTYOU = @"真鍮の腕輪『白鳥』";
        public const string RARE_SINTYUU_RING_AKAHYOU = @"真鍮の腕輪『赤豹』";
        public const string RARE_ICE_SWORD = @"アイシクル・ソード";
        public const string RARE_RISING_KNUCKLE = @"ライジング・ナックル";
        public const string RARE_AUTUMN_ROD = @"オータムン・ロッド";
        public const string RARE_SUN_BRAVE_ARMOR = @"サン・ブレイブアーマー";
        public const string RARE_ESMERALDA_SHIELD = @"シールド・オブ・エスメラルダ";
        #endregion

        #region "ダンジョン１階の宝箱"
        public const string POOR_HARD_SHOES = @"硬質シューズ";
        public const string COMMON_SIMPLE_BRACELET = @"簡素なブレスレット";
        public const string COMMON_SEAL_OF_POSION = @"シール・オブ・ポイズン";
        public const string COMMON_GREEN_EGG_KAIGARA = @"緑卵の貝殻";
        public const string COMMON_CHARM_OF_FIRE_ANGEL = @"炎授天使の護符";
        public const string COMMON_DREAM_POWDER = @"ドリーム・パウダー";
        public const string COMMON_VIKING_SWORD = @"バイキング・ソード";
        public const string COMMON_NEBARIITO_KUMO = @"土蜘蛛の粘り糸";
        public const string COMMON_SUN_PRISM = @"太陽のプリズム";
        public const string COMMON_POISON_EKISU = @"ポイズン・エキス";
        public const string COMMON_SOLID_CLAW = @"ソリッド・クロー";
        public const string COMMON_GREEN_LEEF_CHARM = @"緑葉の魔除け";
        public const string COMMON_WARRIOR_MEDAL = @"戦士の刻印";
        public const string COMMON_PALADIN_MEDAL = @"パラディンの刻印";
        public const string COMMON_KASHI_ROD = @"樫の杖";
        public const string COMMON_HAYATE_ORB = @"疾風の宝珠";
        public const string COMMON_BLUE_COPPER = @"青銅石";
        public const string COMMON_ORANGE_MATERIAL = @"オレンジ・マテリアル";
        public const string RARE_TOTAL_HIYAKU_KASSEI = @"統合秘薬『活性』";
        public const string RARE_ZEPHER_FETHER = @"ゼフィール・フェザー";
        public const string RARE_LIFE_SWORD = @"ソード・オブ・ライフ";
        public const string RARE_PURE_WATER = @"清透水";
        public const string RARE_PURE_GREEN_SILK_ROBE = @"純緑のシルクローブ";
        #endregion

        #region "２階のランダムドロップ"
        public const string POOR_HUANTEI_RING = @"不安定なリング";
        public const string POOR_DEPRESS_FEATHER = @"デプレス・フェザー";
        public const string POOR_DAMAGED_ORB = @"傷アリのオーブ";
        public const string POOR_SHIMETSUKE_BELT = @"締め付けベルト";
        public const string POOR_NOGENKEI_EMBLEM = @"原型の無い紋章";
        public const string POOR_MAGICLIGHT_FIRE = @"マジックライト『火』";
        public const string POOR_MAGICLIGHT_ICE = @"マジックライト『水』";
        public const string POOR_MAGICLIGHT_SHADOW = @"マジックライト『闇』";
        public const string POOR_MAGICLIGHT_LIGHT = @"マジックライト『聖』";
        // 武器のPoorは不要。
        public const string COMMON_RED_CHARM = @"レッド・チャーム";
        public const string COMMON_BLUE_CHARM = @"ブルー・チャーム";
        public const string COMMON_PURPLE_CHARM = @"パープル・チャーム";
        public const string COMMON_GREEN_CHARM = @"グリーン・チャーム";
        public const string COMMON_YELLOW_CHARM = @"イエロー・チャーム";
        public const string COMMON_THREE_COLOR_COMPASS = @"３色コンパス";
        public const string COMMON_SANGO_CROWN = @"珊瑚の冠";
        public const string COMMON_SMOOTHER_BOOTS = @"スムーザー・ブーツ";
        public const string COMMON_SHIOKAZE_MANTLE = @"潮風の外套";
        public const string COMMON_SMART_SWORD = @"スマート・ソード";
        public const string COMMON_SMART_CLAW = @"スマート・クロー";
        public const string COMMON_SMART_ROD = @"スマート・ロッド";
        public const string COMMON_SMART_SHIELD = @"スマート・シールド";
        public const string COMMON_RAUGE_SWORD = @"ラウジェ・ソード";
        public const string COMMON_SMART_CLOTHING = @"スマート・クロス";
        public const string COMMON_SMART_ROBE = @"スマート・ローブ";
        public const string COMMON_SMART_PLATE = @"スマート・プレート";

        public const string RARE_WRATH_SERVEL_CLAW = @"ラス・サーヴェル・クロー";
        public const string RARE_BLUE_LIGHTNING = @"ブルー・ライトニング";
        public const string RARE_DIRGE_ROBE = @"ダージェ・ローブ";
        public const string RARE_DUNSID_PLATE = @"ダンシッド・プレート";
        public const string RARE_BURNING_CLAYMORE = @"バーニング・クレイモア";

        public const string POOR_CURSE_EARRING = @"カース・イヤリング";
        public const string POOR_CURSE_BOOTS = @"呪われたブーツ";
        public const string POOR_BLOODY_STATUE = @"血染めの彫像";
        public const string POOR_FALLEN_MANTLE = @"堕ちたるマント";
        public const string POOR_SIHAIRYU_SIKOTU = @"支配竜の指骨";
        public const string POOR_OLD_TREE_KAREHA = @"古代栄樹の枯れ葉";
        public const string POOR_GALEWIND_KONSEKI = @"ゲイル・ウィンドの痕跡";
        public const string POOR_SIN_CRYSTAL_KAKERA = @"シン・クリスタルの欠片";
        public const string POOR_EVERMIND_ZANSHI = @"エバー・マインドの残思";
        // 武器のPoorは不要。

        public const string COMMON_BRONZE_RING_KIBA = @"青銅の腕輪『牙』";
        public const string COMMON_BRONZE_RING_SASU = @"青銅の腕輪『刺』";
        public const string COMMON_BRONZE_RING_KU = @"青銅の腕輪『駆』";
        public const string COMMON_BRONZE_RING_NAGURI = @"青銅の腕輪『殴』";
        public const string COMMON_BRONZE_RING_TOBI = @"青銅の腕輪『飛』";
        public const string COMMON_BRONZE_RING_KARAMU = @"青銅の腕輪『絡』";
        public const string COMMON_BRONZE_RING_HANERU = @"青銅の腕輪『跳』";
        public const string COMMON_BRONZE_RING_TORU = @"青銅の腕輪『補』";
        public const string COMMON_BRONZE_RING_MIRU = @"青銅の腕輪『視』";
        public const string COMMON_BRONZE_RING_KATAI = @"青銅の腕輪『堅』";
        public const string COMMON_RED_KOKUIN = @"赤の刻印";
        public const string COMMON_BLUE_KOKUIN = @"青の刻印";
        public const string COMMON_PURPLE_KOKUIN = @"紫の刻印";
        public const string COMMON_GREEN_KOKUIN = @"緑の刻印";
        public const string COMMON_YELLOW_KOKUIN = @"黄の刻印";
        public const string COMMON_SISSEI_MANTLE = @"執政のマント";
        public const string COMMON_KAISEI_EMBLEM = @"快晴の紋章";
        public const string COMMON_SAZANAMI_EARRING = @"さざなみイヤリング";
        public const string COMMON_AMEODORI_STATUE = @"雨踊りの彫像";
        public const string COMMON_SMASH_BLADE = @"スマッシュ・ブレード";
        public const string COMMON_POWERED_BUSTER = @"パワード・バスター";
        public const string COMMON_STONE_CLAW = @"ストーン・クロー";
        public const string COMMON_DENDOU_ROD = @"電導ロッド";
        public const string COMMON_SERPENT_ARMOR = @"サーペント・アーマー";
        public const string COMMON_SWIFT_CROSS = @"スウィフト・クロス";
        public const string COMMON_CHIFFON_ROBE = @"シフォン・ローブ";
        public const string COMMON_PURE_BRONZE_SHIELD = @"純青銅の盾";

        public const string RARE_RING_BRONZE_RING_KONSHIN = @"燐青銅の腕輪『渾身』";
        public const string RARE_RING_BRONZE_RING_SYUNSOKU = @"燐青銅の腕輪『俊足』";
        public const string RARE_RING_BRONZE_RING_JUKURYO = @"燐青銅の腕輪『熟慮』";
        public const string RARE_RING_BRONZE_RING_SOUGEN = @"燐青銅の腕輪『爽源』";
        public const string RARE_RING_BRONZE_RING_YUUDAI = @"燐青銅の腕輪『雄大』";
        public const string RARE_MEIUN_BOX = @"命運のプリズムボックス";
        public const string RARE_WILL_HOLY_HAT = @"ウィル・ホーリーズ・ハット";
        public const string RARE_EMBLEM_BLUESTAR = @"エムブレム・オブ・ブルースター";
        public const string RARE_SEAL_OF_DEATH = @"シール・オブ・デス";
        public const string RARE_DARKNESS_SWORD = @"ソード・オブ・ダークネス";
        public const string RARE_BLUE_RED_ROD = @"赤蒼授の杖";
        public const string RARE_SHARKSKIN_ARMOR = @"シャークスキン・アーマー";
        public const string RARE_RED_THUNDER_ROBE = @"ローブ・オブ・レッドサンダー";
        public const string RARE_BLACK_MAGICIAN_CROSS = @"黒魔術師の舞踏衣";
        public const string RARE_BLUE_SKY_SHIELD = @"ブルースカイ・シールド";
        #endregion

        #region "ダンジョン２階の宝箱"
        public const string COMMON_PUZZLE_BOX = @"パズル・ボックス";
        public const string COMMON_CHIENOWA_RING = @"知恵の輪リング";
        public const string RARE_MASTER_PIECE = @"マスター・ピース";
        public const string COMMON_TUMUJIKAZE_BOX = @"つむじ風の箱";
        public const string COMMON_ROCKET_DASH = @"ロケット・ダッシュ";
        public const string COMMON_CLAW_OF_SPRING = @"春風の爪";
        public const string COMMON_SOUKAI_DRINK_WATER = @"爽快ドリンクの原液";
        public const string COMMON_BREEZE_CROSS = @"そよ風の舞踏衣";
        public const string COMMON_GUST_SWORD = @"ソード・オブ・ガスト";
        //public const string RARE_PURE_GREEN_WATER = @"活湧水"; // Duel、ジェダ・アルスの持ち物はここで入手可能とする。
        public const string COMMON_BLANK_BOX = @"空白の箱";
        public const string RARE_SPIRIT_OF_HEART = @"心の聖杯【ハート】";
        public const string COMMON_FUSION_BOX = @"フュージョン・ボックス";
        public const string COMMON_WAR_DRUM = @"ウォードラム";
        public const string COMMON_KOBUSHI_OBJE = @"拳型のオブジェ";
        public const string COMMON_TIGER_BLADE = @"タイガー・ブレイド";
        public const string COMMON_TUUKAI_DRINK_WATER = @"痛快ドリンクの原液";
        public const string RARE_ROD_OF_STRENGTH = @"力の杖";
        public const string RARE_SOUJUTENSHI_NO_GOFU = @"蒼授天使の護符";
        public const string RARE_SINSEISUI = @"神聖水";
        #endregion

        #region "３階のランダムドロップ"
        // 武器のPoorは不要。
        public const string POOR_DIRTY_ANGEL_CONTRACT = @"ボロボロになった天使の契約書";
        public const string POOR_JUNK_TARISMAN_FROZEN = @"ジャンク・タリスマン【凍結】";
        public const string POOR_JUNK_TARISMAN_PARALYZE = @"ジャンク・タリスマン【麻痺】";
        public const string POOR_JUNK_TARISMAN_STUN = @"ジャンク・タリスマン【スタン】";
        public const string COMMON_RED_STONE = @"レッド・ストーン";
        public const string COMMON_BLUE_STONE = @"ブルー・ストーン";
        public const string COMMON_PURPLE_STONE = @"パープル・ストーン";
        public const string COMMON_GREEN_STONE = @"グリーン・ストーン";
        public const string COMMON_YELLOW_STONE = @"イエロー・ストーン";
        public const string COMMON_EXCELLENT_SWORD = @"エクセレント・ソード";
        public const string COMMON_EXCELLENT_KNUCKLE = @"エクセレント・ナックル";
        public const string COMMON_EXCELLENT_ROD = @"エクセレント・ロッド";
        public const string COMMON_EXCELLENT_BUSTER = @"エクセレント・バスター";
        public const string COMMON_EXCELLENT_ARMOR = @"エクセレント・アーマー";
        public const string COMMON_EXCELLENT_CROSS = @"エクセレント・クロス";
        public const string COMMON_EXCELLENT_ROBE = @"エクセレント・ローブ";
        public const string COMMON_EXCELLENT_SHIELD = @"エクセレント・シールド";
        public const string RARE_MENTALIZED_FORCE_CLAW = @"メンタライズド・フォース・クロー";
        public const string RARE_ADERKER_FALSE_ROD = @"アダーカー・フォルス・ロッド";
        public const string RARE_BLACK_ICE_SWORD = @"黒氷刀";
        public const string RARE_CLAYMORE_ZUKS = @"クレイモア・オブ・ザックス";
        public const string RARE_DRAGONSCALE_ARMOR = @"ドラゴンスケイル・アーマー";
        public const string RARE_LIGHT_BLIZZARD_ROBE = @"ライトブリザード・ローブ";

        public const string POOR_MIGAWARI_DOOL = @"身代わり人形";
        public const string POOR_ONE_DROPLET_KESSYOU = @"一滴の雫結晶";
        public const string POOR_MOMENTARY_FLASH_LIGHT = @"モーメンタリ・フラッシュ";
        public const string POOR_SUN_YUME_KAKERA = @"寸の夢の欠片";
        public const string COMMON_STEEL_RING_1 = @"鋼の腕輪『パワー』";
        public const string COMMON_STEEL_RING_2 = @"鋼の腕輪『センス』";
        public const string COMMON_STEEL_RING_3 = @"鋼の腕輪『タフ』";
        public const string COMMON_STEEL_RING_4 = @"鋼の腕輪『ロック』";
        public const string COMMON_STEEL_RING_5 = @"鋼の腕輪『ファスト』";
        public const string COMMON_STEEL_RING_6 = @"鋼の腕輪『シャープ』";
        public const string COMMON_STEEL_RING_7 = @"鋼の腕輪『ハイ』";
        public const string COMMON_STEEL_RING_8 = @"鋼の腕輪『ディープ』";
        public const string COMMON_STEEL_RING_9 = @"鋼の腕輪『バウンド』";
        public const string COMMON_STEEL_RING_10 = @"鋼の腕輪『エモート』";
        public const string COMMON_RED_MASEKI = @"赤の魔石";
        public const string COMMON_BLUE_MASEKI = @"青の魔石";
        public const string COMMON_PURPLE_MASEKI = @"紫の魔石";
        public const string COMMON_GREEN_MASEKI = @"緑の魔石";
        public const string COMMON_YELLOW_MASEKI = @"黄の魔石";
        public const string COMMON_DESCENED_BLADE = @"ディッセンド・ブレード";
        public const string COMMON_FALSET_CLAW = @"ファルセットの爪";
        public const string COMMON_SEKIGAN_ROD = @"隻眼の杖";
        public const string COMMON_ROCK_BUSTER = @"ロック・バスター";
        public const string COMMON_COLD_STEEL_PLATE = @"コールド・スチール・プレート";
        public const string COMMON_AIR_HARE_CROSS = @"空晴の舞踏衣";
        public const string COMMON_FLOATING_ROBE = @"フローティング・ローブ";
        public const string COMMON_SNOW_CRYSTAL_SHIELD = @"雪結晶の盾";
        public const string COMMON_WING_STEP_FEATHER = @"ウィングステップ・フェザー";
        public const string COMMON_SNOW_FAIRY_BREATH = @"スノーフェアリーの息吹";
        public const string COMMON_STASIS_RING = @"ステイシス・リング";
        public const string COMMON_SIHAIRYU_KIBA = @"支配竜の牙";
        public const string COMMON_OLD_TREE_JUSHI = @"古代栄樹の樹脂";
        public const string COMMON_GALEWIND_KIZUATO = @"ゲイル・ウィンドの傷跡";
        public const string COMMON_SIN_CRYSTAL_QUATZ = @"シン・クリスタル・クォーツ";
        public const string COMMON_EVERMIND_OMEN = @"エバー・マインド・オーメン";
        public const string RARE_FROZEN_LAVA = @"凍結した溶岩";
        public const string RARE_WHITE_TIGER_ANGEL_GOHU = @"珀虎天使の護符";
        public const string RARE_POWER_STEEL_RING_SOLID = @"強芯鋼の腕輪『ソリッド』";
        public const string RARE_POWER_STEEL_RING_VAPOR = @"強芯鋼の腕輪『ヴェイパー』";
        public const string RARE_POWER_STEEL_RING_ERASTIC = @"強芯鋼の腕輪『エラスト』";
        public const string RARE_POWER_STEEL_RING_TORAREITION = @"強芯鋼の腕輪『トラレイス』";
        public const string RARE_SYUURENSYA_KUROOBI = @"修練者の黒帯";
        public const string RARE_SHIHANDAI_KUROOBI = @"師範代の黒帯";
        public const string RARE_SYUUDOUSOU_KUROOBI = @"修道僧の黒帯";
        public const string RARE_KUGYOUSYA_KUROOBI = @"苦行者の黒帯";
        public const string RARE_TEARS_END = @"ティアーズ・エンド";
        public const string RARE_SKY_COLD_BOOTS = @"スカイ・コールド・ブーツ";
        public const string RARE_EARTH_BREAKERS_SIGIL = @"アース・ブレイカーズ・シギル";
        public const string RARE_AERIAL_VORTEX = @"エアリアル・ヴォルテックス";
        public const string RARE_LIVING_GROWTH_SEED = @"リヴィング・グロース・シード";
        public const string RARE_SHARPNEL_SPIN_BLADE = @"シャープネル・スピン・ブレイド";
        public const string RARE_BLUE_LIGHT_MOON_CLAW = @"薄青く光る蒼月爪";
        public const string RARE_BLIZZARD_SNOW_ROD = @"ブリザード・スノー・ロッド";
        public const string RARE_SHAERING_BONE_CRUSHER = @"シアリング・ボーン・クラッシャー";
        public const string RARE_SCALE_BLUERAGE = @"スケイル・オブ・ブルーレイジ";
        public const string RARE_BLUE_REFLECT_ROBE = @"ブルー・リフレクト・ローブ";
        public const string RARE_SLIDE_THROUGH_SHIELD = @"スライド・スルー・シールド";
        public const string RARE_ELEMENTAL_STAR_SHIELD = @"エレメンタル・スター・シールド";
        #endregion

        #region "ダンジョン３階の宝箱"
        public const string COMMON_ESSENCE_OF_EARTH = @"エッセンス・オブ・アース";
        public const string COMMON_KESSYOU_SEA_WATER_SALT = @"結晶化した海水塩";
        public const string COMMON_STAR_DUST_RING = @"スターダスト・リング";
        public const string COMMON_RED_ONION = @"赤タマネギ";
        public const string RARE_TAMATEBAKO_AKIDAMA = @"玉手箱『秋玉』";
        public const string RARE_HARDEST_FIT_BOOTS = @"ハーデスト・フィット・ブーツ";
        public const string COMMON_WATERY_GROBE = @"ウォータリー・グローヴ";
        public const string COMMON_WHITE_POWDER = @"ホワイト・パウダー";
        public const string COMMON_SILENT_BOWL = @"サイレント・ボール";
        public const string RARE_SEAL_OF_ICE = @"シール・オヴ・アイス";
        public const string RARE_SWORD_OF_DIVIDE = @"ソード・オブ・ディバイド";
        public const string EPIC_OLD_TREE_MIKI_DANPEN = @"古代栄樹の幹の断片";
        #endregion

        #region "４階のランダムドロップ"
        // 武器のPoorは不要。
        //public const string RARE_PURPLE_ABYSSAL_SWORD = @"パープル・アヴィッサル・ソード";
        //public const string RARE_BLACK_HIEN_CLAW = @"黒飛燕の爪";
        //public const string POOR_DIRTY_ANGEL_CONTRACT = @"ボロボロになった天使の契約書";
        //public const string POOR_JUNK_TARISMAN_FROZEN = @"ジャンク・タリスマン【凍結】";
        //public const string POOR_JUNK_TARISMAN_PARALYZE = @"ジャンク・タリスマン【麻痺】";
        //public const string POOR_JUNK_TARISMAN_STUN = @"ジャンク・タリスマン【スタン】";
        public const string COMMON_RED_MEDALLION = @"レッド・メダリオン";
        public const string COMMON_BLUE_MEDALLION = @"ブルー・メダリオン";
        public const string COMMON_PURPLE_MEDALLION = @"パープル・メダリオン";
        public const string COMMON_GREEN_MEDALLION = @"グリーン・メダリオン";
        public const string COMMON_YELLOW_MEDALLION = @"イエロー・メダリオン";
        public const string COMMON_SOCIETY_SYMBOL = @"ソサエティ・シンボル";
        public const string COMMON_SILVER_FEATHER_BRACELET = @"銀羽飾りの腕輪";
        public const string COMMON_BIRD_SONG_LUTE = @"バード・ソング・リュート";
        public const string COMMON_MAZE_CUBE = @"メイズ・キューブ";
        public const string COMMON_LIGHT_SERVANT = @"ライト・サーヴァント";
        public const string COMMON_SHADOW_SERVANT = @"シャドウ・サーヴァント";
        public const string COMMON_ROYAL_GUARD_RING = @"ロイヤル・ガード・リング";
        public const string COMMON_ELEMENTAL_GUARD_RING = @"エレメンタル・ガード・リング";
        public const string COMMON_HAYATE_GUARD_RING = @"ハヤテ・ガード・リング";
        public const string RARE_SPELL_COMPASS = @"詠唱の羅針盤";
        public const string RARE_SHADOW_BIBLE = @"闇のバイブル";
        public const string RARE_DETACHMENT_ORB = @"デタッチメント・オーブ";
        public const string RARE_BLIND_NEEDLE = @"盲目者の針";
        public const string RARE_CORE_ESSENCE_CHANNEL = @"コア・エッセンス・チャネル";
        public const string COMMON_MASTER_SWORD = @"マスター・ソード";
        public const string COMMON_MASTER_KNUCKLE = @"マスター・ナックル";
        public const string COMMON_MASTER_ROD = @"マスター・ロッド";
        public const string COMMON_MASTER_AXE = @"マスター・アックス";
        public const string COMMON_MASTER_ARMOR = @"マスター・アーマー";
        public const string COMMON_MASTER_CROSS = @"マスター・クロス";
        public const string COMMON_MASTER_ROBE = @"マスター・ローブ";
        public const string COMMON_MASTER_SHIELD = @"マスター・シールド";
        public const string RARE_ASTRAL_VOID_BLADE = @"アストラル・ヴォイド・ブレード";
        public const string RARE_VERDANT_SONIC_CLAW = @"ヴェルダント・ソニック・クロー";
        public const string RARE_PRISONER_BREAKING_AXE = @"プリズナー・ブレイキング・アックス";
        public const string RARE_INVISIBLE_STATE_ROD = @"インヴィジブル・ステイト・ロッド";
        public const string RARE_DOMINATION_BRAVE_ARMOR = @"ドミネーション・ブレイブ・アーマー";

        public const string COMMON_RED_FLOAT_STONE = @"赤の浮遊石";
        public const string COMMON_BLUE_FLOAT_STONE = @"青の浮遊石";
        public const string COMMON_PURPLE_FLOAT_STONE = @"紫の浮遊石";
        public const string COMMON_GREEN_FLOAT_STONE = @"緑の浮遊石";
        public const string COMMON_YELLOW_FLOAT_STONE = @"黄の浮遊石";
        public const string COMMON_SILVER_RING_1 = @"銀の腕輪【業火】";
        public const string COMMON_SILVER_RING_2 = @"銀の腕輪【津波】";
        public const string COMMON_SILVER_RING_3 = @"銀の腕輪【秋雨】";
        public const string COMMON_SILVER_RING_4 = @"銀の腕輪【熱波】";
        public const string COMMON_SILVER_RING_5 = @"銀の腕輪【雷鳴】";
        public const string COMMON_SILVER_RING_6 = @"銀の腕輪【吹雪】";
        public const string COMMON_SILVER_RING_7 = @"銀の腕輪【幻日】";
        public const string COMMON_SILVER_RING_8 = @"銀の腕輪【竜巻】";
        public const string COMMON_SILVER_RING_9 = @"銀の腕輪【主虹】";
        public const string COMMON_SILVER_RING_10 = @"銀の腕輪【陽炎】";
        public const string COMMON_MUKEI_SAKAZUKI = @"無形の盃";
        public const string COMMON_RAINBOW_TUBE = @"レインボー・チューブ";
        public const string COMMON_ELDER_PERSPECTIVE_GRASS = @"エルダー・パースペクティブ・グラス";
        public const string COMMON_DEVIL_SEALED_VASE = @"悪魔封じの壺";
        public const string COMMON_FLOATING_WHITE_BALL = @"フローティング・ホワイト・ボール";
        public const string RARE_SEAL_OF_ASSASSINATION = @"シール・オブ・アサシネーション";
        public const string RARE_EMBLEM_OF_VALKYRIE = @"エムブレム・オブ・ヴァルキリー";
        public const string RARE_EMBLEM_OF_HADES = @"エムブレム・オブ・ハデス";
        public const string RARE_SIHAIRYU_KATAUDE = @"支配竜の片腕";
        public const string RARE_OLD_TREE_SINKI = @"古代栄樹の芯木";
        public const string RARE_GALEWIND_IBUKI = @"ゲイル・ウィンドの息吹";
        public const string RARE_SIN_CRYSTAL_SOLID = @"シン・クリスタル・ソリッド";
        public const string RARE_EVERMIND_SENSE = @"エバー・マインド・センス";
        public const string RARE_DEVIL_SUMMONER_TOME = @"デビル・サモナーズ・トーム";
        public const string RARE_ANGEL_CONTRACT = @"天使の契約書";
        public const string RARE_ARCHANGEL_CONTRACT = @"大天使の契約書";
        public const string RARE_DARKNESS_COIN = @"暗黒の通貨";
        public const string RARE_SOUSUI_HIDENSYO = @"総帥の秘伝書";
        public const string RARE_MEEK_HIDENSYO = @"弱者の秘伝書";
        public const string RARE_JUKUTATUSYA_HIDENSYO = @"熟達者の秘伝書";
        public const string RARE_KYUUDOUSYA_HIDENSYO = @"求道者の秘伝書";
        public const string RARE_DANZAI_ANGEL_GOHU = @"断罪天使の護符";
        public const string EPIC_ETERNAL_HOMURA_RING = @"不死なる焔火のリング";

        public const string COMMON_INITIATE_SWORD = @"イニシエイト・ソード";
        public const string COMMON_BULLET_KNUCKLE = @"バレット・ナックル";
        public const string COMMON_KENTOUSI_SWORD = @"剣闘士の大剣";
        public const string COMMON_ELECTRO_ROD = @"エレクトロ・ロッド";
        public const string COMMON_FORTIFY_SCALE = @"フォーティファイ・スケイル";
        public const string COMMON_MURYOU_CROSS = @"無量の舞踏衣";
        public const string COMMON_COLORLESS_ROBE = @"カラレス・ローブ";
        public const string COMMON_LOGISTIC_SHIELD = @"ロジスティック・シールド";
        public const string RARE_ETHREAL_EDGE_SABRE = @"イスリアル・エッジ・サーベル";
        public const string RARE_SHINGETUEN_CLAW = @"深月淵の爪";
        public const string RARE_BLOODY_DIRTY_SCYTHE = @"ブラッディ・ダーティ・サイ";
        public const string RARE_ALL_ELEMENTAL_ROD = @"オール・エレメンタル・ロッド";
        public const string RARE_BLOOD_BLAZER_CROSS = @"ブラッド・ブレイザー・クロス";
        public const string RARE_DARK_ANGEL_ROBE = @"ダーク・エンジェル・ローブ";
        public const string RARE_MAJEST_HAZZARD_SHIELD = @"マジェスト・ハザード・シールド";
        public const string RARE_WHITE_DIAMOND_SHIELD = @"白泊色のダイヤ・シールド";
        public const string RARE_VAPOR_SOLID_SHIELD = @"ヴェイパー・ソリッド・シールド";
        #endregion

        #region "ダンジョン４階の宝箱"
        public const string COMMON_BLACK_SALT = @"黒く変色した岩状の塊";
        public const string COMMON_FEBL_ANIS = @"フェブル・アニス";
        public const string COMMON_SMORKY_HUNNY = @"スモーキー・ハニー";
        public const string COMMON_ANGEL_DUST = @"エンジェル・ダスト";
        public const string COMMON_SUN_TARAGON = @"サン・タラゴン";
        public const string COMMON_ECHO_BEAST_MEAT = @"エコービーストのもも肉";
        public const string COMMON_CHAOS_TONGUE = @"カオス・ワーデンの舌";
        public const string RARE_JOUKA_TANZOU = @"浄火の鍛造";
        public const string RARE_ESSENCE_OF_ADAMANTINE = @"エッセンス・オブ・アダマンティ";
        #endregion

        #region "５階のランダムドロップ"
        public const string COMMON_RED_CRYSTAL = @"真紅のクリスタル";
        public const string COMMON_BLUE_CRYSTAL = @"瑠璃のクリスタル";
        public const string COMMON_PURPLE_CRYSTAL = @"紫苑のクリスタル";
        public const string COMMON_GREEN_CRYSTAL = @"翡翠のクリスタル";
        public const string COMMON_YELLOW_CRYSTAL = @"琥珀のクリスタル";
        public const string COMMON_PLATINUM_RING_1 = @"白金の腕輪【白虎】";
        public const string COMMON_PLATINUM_RING_2 = @"白金の腕輪【ヴァルキリー】";
        public const string COMMON_PLATINUM_RING_3 = @"白金の腕輪【ナイトメア】";
        public const string COMMON_PLATINUM_RING_4 = @"白金の腕輪【ナラシンハ】";
        public const string COMMON_PLATINUM_RING_5 = @"白金の腕輪【朱雀】";
        public const string COMMON_PLATINUM_RING_6 = @"白金の腕輪【ウロボロス】";
        public const string COMMON_PLATINUM_RING_7 = @"白金の腕輪【ナインテイル】";
        public const string COMMON_PLATINUM_RING_8 = @"白金の腕輪【ベヒモス】";
        public const string COMMON_PLATINUM_RING_9 = @"白金の腕輪【青龍】";
        public const string COMMON_PLATINUM_RING_10 = @"白金の腕輪【玄武】";
        #endregion

        #region "ダンジョン現実世界の宝箱"
        public const string EPIC_GOLD_POTION = @"黄金樹の秘薬";
        public const string EPIC_OVER_SHIFTING = @"オーバーシフティング";
        #endregion

        #region "各階のボス撃破時"
        public const string EPIC_ORB_GROW_GREEN = @"新成緑の宝珠";
        public const string EPIC_ORB_GROUNDSEA_STAR = @"海星源の宝珠";
        public const string EPIC_ORB_SILENT_COLD_ICE = @"氷絶零の宝珠";
        public const string EPIC_ORB_DESTRUCT_FIRE = @"焔浄痕の宝珠"; // "闇厳焔の宝珠"
        #endregion

        #region "EPIC"
        // EPIC１
        public const string EPIC_RING_OF_OSCURETE = @"Ring of the Oscurete";
        public const string EPIC_MERGIZD_SOL_BLADE = @"Mergizd Sol Blade";
        // EPIC２
        public const string EPIC_GARVANDI_ADILORB = @"AdilOrb of the Garvandi";
        public const string EPIC_MAXCARN_X_BUSTER = @"Maxcarn the X-BUSTER";
        public const string EPIC_JUZA_ARESTINE_SLICER = @"Arestine-Slicer of Juza";
        // EPIC３
        public const string EPIC_SHEZL_MYSTIC_FORTUNE = @"Shezl the Mystic Fortune";
        public const string EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE = @"Flow Funnel of the Zveldose";
        public const string EPIC_MERGIZD_DAV_AGITATED_BLADE = @"Mergizd DAV-Agitated Blade";
        // EPIC４
        public const string EPIC_EZEKRIEL_ARMOR_SIGIL = @"Ezekriel the Armor of Sigil";
        public const string EPIC_SHEZL_THE_MIRAGE_LANCER = @"Shezl the Mirage Lancer";
        public const string EPIC_JUZA_THE_PHANTASMAL_CLAW = @"Juza the Phantasmal Claw";
        public const string EPIC_ADILRING_OF_BLUE_BURN = @"AdilRing of the Blue Burn";
        #endregion

        // ジェム
        // ストーン
        // ジュエル
        // フランベルジュ
        #region "成長剤"
        // 成長剤（１階）
        public const string GROWTH_LIQUID_STRENGTH = @"成長リキッド【力】";
        public const string GROWTH_LIQUID_AGILITY = @"成長リキッド【技】";
        public const string GROWTH_LIQUID_INTELLIGENCE = @"成長リキッド【知】";
        public const string GROWTH_LIQUID_STAMINA = @"成長リキッド【体】";
        public const string GROWTH_LIQUID_MIND = @"成長リキッド【心】";
        // 成長剤（２階）
        public const string GROWTH_LIQUID2_STRENGTH = @"成長リキッドⅡ【力】";
        public const string GROWTH_LIQUID2_AGILITY = @"成長リキッドⅡ【技】";
        public const string GROWTH_LIQUID2_INTELLIGENCE = @"成長リキッドⅡ【知】";
        public const string GROWTH_LIQUID2_STAMINA = @"成長リキッドⅡ【体】";
        public const string GROWTH_LIQUID2_MIND = @"成長リキッドⅡ【心】";
        // 成長剤（３階）
        public const string GROWTH_LIQUID3_STRENGTH = @"成長リキッドⅢ【力】";
        public const string GROWTH_LIQUID3_AGILITY = @"成長リキッドⅢ【技】";
        public const string GROWTH_LIQUID3_INTELLIGENCE = @"成長リキッドⅢ【知】";
        public const string GROWTH_LIQUID3_STAMINA = @"成長リキッドⅢ【体】";
        public const string GROWTH_LIQUID3_MIND = @"成長リキッドⅢ【心】";
        // 成長剤（４階）
        public const string GROWTH_LIQUID4_STRENGTH = @"成長リキッドⅣ【力】";
        public const string GROWTH_LIQUID4_AGILITY = @"成長リキッドⅣ【技】";
        public const string GROWTH_LIQUID4_INTELLIGENCE = @"成長リキッドⅣ【知】";
        public const string GROWTH_LIQUID4_STAMINA = @"成長リキッドⅣ【体】";
        public const string GROWTH_LIQUID4_MIND = @"成長リキッドⅣ【心】";
        // 成長剤（５階）
        public const string GROWTH_LIQUID5_STRENGTH = @"成長リキッドⅤ【力】";
        public const string GROWTH_LIQUID5_AGILITY = @"成長リキッドⅤ【技】";
        public const string GROWTH_LIQUID5_INTELLIGENCE = @"成長リキッドⅤ【知】";
        public const string GROWTH_LIQUID5_STAMINA = @"成長リキッドⅤ【体】";
        public const string GROWTH_LIQUID5_MIND = @"成長リキッドⅤ【心】";
        #endregion

        #region "無価値アイテム"
        public const string POOR_BLACK_MATERIAL = @"ブラックマテリアル";
        public const string POOR_BLACK_MATERIAL2 = @"ブラックマテリアル【改】";
        public const string POOR_BLACK_MATERIAL3 = @"ブラックマテリアル【密】";
        public const string POOR_BLACK_MATERIAL4 = @"ブラックマテリアル【試】";
        public const string POOR_BLACK_MATERIAL5 = @"ブラックマテリアル【還】";
        #endregion

        public const string RARE_SEAL_AQUA_FIRE = @"シールオブアクア＆ファイア";

        #region "後編、１階の素材系ドロップアイテム"
        public const string COMMON_BEATLE_TOGATTA_TUNO = @"ビートルの尖った角";
        public const string COMMON_HENSYOKU_KUKI = @"変色した茎";
        public const string COMMON_GREEN_SIKISO = @"緑化色素";
        public const string COMMON_MANTIS_TAIEKI = @"マンティスの体液";
        public const string COMMON_WARM_NO_KOUKAKU = @"ワームの甲殻";
        public const string COMMON_MANDORAGORA_ROOT = @"マンドラゴラの根";

        public const string COMMON_SUN_LEAF = @"太陽の葉";
        public const string COMMON_INAGO = @"蝗";
        public const string COMMON_SPIDER_SILK = @"スパイダーシルク";
        public const string COMMON_ANT_ESSENCE = @"アントのエキス";
        public const string COMMON_YELLOW_MATERIAL = @"イエロー・マテリアル";
        public const string COMMON_ALRAUNE_KAHUN = @"アルラウネの花粉";
        public const string RARE_MARY_KISS = @"マリーキッス";

        public const string COMMON_RABBIT_KEGAWA = @"ウサギの毛皮";
        public const string COMMON_RABBIT_MEAT = @"ウサギの肉";
        public const string COMMON_TAKA_FETHER = @"鷹の白羽";
        public const string COMMON_ASH_EGG = @"薄灰色の卵";
        public const string COMMON_SNEAK_UROKO = @"ヘビの鱗";
        public const string COMMON_PLANTNOID_SEED = @"プラントノイドの種";
        public const string COMMON_TOGE_HAETA_SYOKUSYU = @"刺の生えた触手";
        public const string RARE_HYUI_SEED = @"ヒューイの種";

        public const string COMMON_OOKAMI_FANG = @"狼の牙";
        public const string COMMON_BRILLIANT_RINPUN = @"輝きの燐粉";
        public const string COMMON_MIST_CRYSTAL = @"霧の結晶";
        public const string COMMON_DRYAD_RINPUN = @"ドライアドの鱗粉";
        public const string COMMON_RED_HOUSI = @"赤い胞子";
        public const string RARE_MOSSGREEN_EKISU = @"モスグリーンのエキス";
        #endregion

        #region "後編、２階、素材ドロップ"
        public const string COMMON_DAGGERFISH_UROKO = @"牙魚の鱗";
        public const string COMMON_SIPPUU_HIRE = @"疾魚のヒレ";
        public const string COMMON_WHITE_MAGATAMA = @"白の勾玉";
        public const string COMMON_BLUE_MAGATAMA = @"青の勾玉";
        public const string COMMON_KURIONE_ZOUMOTU = @"クリオネの臓物";
        public const string COMMON_BLUEWHITE_SHARP_TOGE = @"青白の鋭いトゲ";
        public const string RARE_TRANSPARENT_POWDER = @"透明なパウダー";

        public const string COMMON_RENEW_AKAMI = @"新鮮な赤身";
        public const string COMMON_SEA_WASI_KUTIBASI = @"海鷲のくちばし";
        public const string COMMON_WASI_BLUE_FEATHER = @"鷲の青羽";
        public const string COMMON_BRIGHT_GESO = @"光るゲソ";
        public const string COMMON_GANGAME_EGG = @"頑亀の卵";
        public const string RARE_JOE_TONGUE = @"ジョーの舌";
        public const string RARE_JOE_ARM = @"ジョーの腕";
        public const string RARE_JOE_LEG = @"ジョーの足";

        public const string COMMON_SOFT_BIG_HIRE = @"柔らかい大ヒレ";
        public const string COMMON_PURE_WHITE_BIGEYE = @"真っ白な大目玉";
        public const string COMMON_GOTUGOTU_KARA = @"ゴツゴツした殻";
        public const string COMMON_SAME_NANKOTSU = @"サメの軟骨";
        public const string COMMON_HALF_TRANSPARENT_ROCK_ASH = @"半透明の石灰";
        public const string RARE_SEKIKASSYOKU_HASAMI = @"赤褐色のハサミ";

        public const string COMMON_KOUSITUKA_MATERIAL = @"硬質化素材";
        public const string COMMON_NANAIRO_SYOKUSYU = @"七色の触手";
        public const string COMMON_PUREWHITE_KIMO = @"真っ白なキモ";
        public const string COMMON_AOSAME_KENSHI = @"青鮫の剣歯";
        public const string COMMON_AOSAME_UROKO = @"青鮫の鱗";
        public const string COMMON_EIGHTEIGHT_KUROSUMI = @"エイト・エイトの黒墨";
        public const string COMMON_EIGHTEIGHT_KYUUBAN = @"エイト・エイトの吸盤";
        #endregion

        #region "後編、３階、素材ドロップ"
        public const string COMMON_ORC_MOMONIKU = @"オークのもも肉";
        public const string COMMON_SNOW_CAT_KEGAWA = @"雪猫の毛皮";
        public const string COMMON_BIG_HIZUME = @"大きな蹄";
        public const string COMMON_FAIRY_POWDER = @"妖精パウダー";
        public const string RARE_GRIFFIN_WHITE_FEATHER = @"グリフィンの白い羽";

        public const string COMMON_GOTUGOTU_KONBOU = @"ゴツゴツした棍棒";
        public const string COMMON_LIZARD_UROKO = @"リザードの鱗";
        public const string COMMON_EMBLEM_OF_PENGUIN = @"エムブレム・オブ・ペンギン";
        public const string COMMON_KINKIN_ICE = @"キンキンに冷えた氷";
        public const string COMMON_SHARPNESS_TIGER_TOOTH = @"鋭く尖った虎牙";
        public const string RARE_BEAR_CLAW_KAKERA = @"ベアクローの欠片";

        public const string COMMON_TOUMEI_SNOW_CRYSTAL = @"透明な雪結晶";
        public const string COMMON_WHITE_AZARASHI_MEAT = @"白アザラシの肉";
        public const string COMMON_CENTAURUS_LEATHER = @"ケンタウルスの皮";
        public const string COMMON_ARGONIAN_PURPLE_UROKO = @"アルゴニアンの紫鱗";
        public const string COMMON_BLUE_DANGAN_KAKERA = @"蒼い弾丸の欠片";
        public const string RARE_PURE_CRYSTAL = @"ピュア・クリスタル";

        public const string COMMON_WOLF_KEGAWA = @"ウルフの毛皮";
        public const string COMMON_FROZEN_HEART = @"凍結した心臓";
        public const string COMMON_CLAW_HEART = @"グリズリーの心臓";
        public const string COMMON_ESSENCE_OF_WIND = @"エッセンス・オヴ・ウィンド";
        public const string RARE_TUNDRA_DEER_HORN = @"古代ツンドラ鹿の角";
        #endregion

        #region "後編、４階、素材ドロップ"
        public const string COMMON_HUNTER_SEVEN_TOOL = @"ハンターの七つ道具";
        public const string COMMON_BEAST_KEGAWA = @"猛獣の毛皮";
        public const string RARE_BLOOD_DAGGER_KAKERA = @"血塗られたダガーの破片";
        public const string COMMON_SABI_BUGU = @"錆付いたガラクタ武具";
        public const string RARE_MEPHISTO_BLACKLIGHT = @"メフィストの黒い灯";

        public const string COMMON_SEEKER_HEAD = @"シーカーの頭蓋骨";
        public const string RARE_ESSENCE_OF_DARK = @"エッセンス・オブ・ダーク";
        public const string COMMON_EXECUTIONER_ROBE = @"執行人の汚れたローブ";
        public const string COMMON_NEMESIS_ESSENCE = @"ネメシス・エッセンス";
        public const string RARE_MASTERBLADE_KAKERA = @"マスターブレイドの破片";
        public const string RARE_MASTERBLADE_FIRE = @"マスターブレイドの残り火";
        public const string COMMON_GREAT_JEWELCROWN = @"豪華なジュエルクラウン";

        public const string RARE_ESSENCE_OF_SHINE = @"エッセンス・オブ・シャイン";
        public const string RARE_DEMON_HORN = @"デーモンホーン";
        public const string COMMON_KUMITATE_TENBIN = @"組み立て素材　天秤";
        public const string COMMON_KUMITATE_TENBIN_DOU = @"組み立て素材　天分銅";
        public const string COMMON_KUMITATE_TENBIN_BOU = @"組み立て素材　天秤棒";
        public const string COMMON_WYVERN_BONE = @"ワイバーン・ボーン";
        public const string RARE_ESSENCE_OF_FLAME = @"エッセンス・オブ・フレイム";
        public const string RARE_BLACK_SEAL_IMPRESSION = @"黒の印鑑";
        
        public const string COMMON_ONRYOU_HAKO = @"怨霊箱";
        public const string RARE_ANGEL_SILK = @"天使のシルク";
        public const string RARE_CHAOS_SIZUKU = @"混沌の雫";
        public const string RARE_DREAD_EXTRACT = @"ドレッド・エキス";
        public const string RARE_DOOMBRINGER_TUKA = @"ドゥームブリンガーの柄";
        public const string RARE_DOOMBRINGER_KAKERA = @"ドゥームブリンガーの欠片";
        #endregion

        #region "後編、ガンツの武具屋１階"
        // 初版
        public const string COMMON_BRONZE_SWORD = @"ブロンズ・ソード";
        public const string COMMON_FIT_ARMOR = @"フィット・アーマー";
        public const string COMMON_LIGHT_SHIELD = @"ライト・シールド";
        public const string COMMON_FINE_SWORD_1 = @"ファイン・ソード【＋１】";
        public const string COMMON_FINE_ARMOR_1 = @"ファイン・アーマー【＋１】";
        public const string COMMON_FINE_SHIELD_1 = @"ファイン・シールド【＋１】";
        public const string COMMON_LIGHT_CLAW_1 = @"ライト・クロー【＋１】";
        public const string COMMON_KASHI_ROD_1 = @"樫の杖【＋１】";
        public const string COMMON_LETHER_CLOTHING_1 = @"レザー・クロス【＋１】";
        public const string COMMON_BASTARD_SWORD_1 = @"バスタード・ソード【＋１】";
        public const string COMMON_IRON_SWORD = @"アイアン・ソード【＋２】";
        public const string COMMON_KUSARI_KATABIRA = @"鎖かたびら【＋１】";
        public const string RARE_FLOWER_WAND = @"フラワー・ワンド";
        public const string COMMON_SUPERIOR_CROSS = @"スペリオル・クロス";
        public const string COMMON_SILK_ROBE = @"シルク・ローブ";
        public const string COMMON_SURVIVAL_CLAW = @"サバイバル・クロー";
        public const string COMMON_BLACER_OF_SYOJIN = @"精進のブレスレット";
        public const string COMMON_ZIAI_PENDANT = @"慈愛のペンダント";
        // 合成
        public const string COMMON_KOUKAKU_ARMOR = @"甲殻の鎧";
        public const string COMMON_SISSO_TUKEHANE = @"質素な付け羽";
        public const string RARE_WAR_WOLF_BLADE = @"ワーウルフ・ブレード";
        public const string COMMON_BLUE_COPPER_ARMOR_KAI = @"青銅の鎧【改】";
        public const string COMMON_RABBIT_SHOES = @"ラビット・シューズ";
        public const string RARE_MISTSCALE_SHIELD = @"ミストスケイル・シールド";
        #endregion
        #region "後編、ガンツの武具屋２階"
        // ２階
        // 初版
        public const string COMMON_SMART_SWORD_2 = @"スマート・ソード【＋２】";
        public const string COMMON_SMART_PLATE_2 = @"スマート・プレート【＋２】";
        public const string COMMON_SMART_SHIELD_2 = @"スマート・シールド【＋２】";
        public const string COMMON_RAUGE_SWORD_2 = @"ラウジェ・ソード【＋２】";
        public const string COMMON_SMART_CLAW_2 = @"スマート・クロー【＋２】";
        public const string COMMON_SMART_ROD_2 = @"スマート・ロッド【＋２】";
        public const string COMMON_SMART_CLOTHING_2 = @"スマート・クロス【＋２】";
        public const string COMMON_SMART_ROBE_2 = @"スマート・ローブ【＋２】";
        public const string COMMON_STEEL_SWORD = @"スチール・ソード【＋３】";
        public const string COMMON_FACILITY_CLAW = @"ファシリティ・クロー【＋３】";
        public const string COMMON_MIX_HINOKI_ROD = @"合成檜の杖";
        public const string COMMON_BERSERKER_PLATE = @"バーサーカープレート";
        public const string COMMON_BRIGHTNESS_ROBE = @"ブライトネス・ローブ";
        public const string RARE_WILD_HEART_SPADE = @"ワイルド・ハート【スペード】";

        // 合成
        public const string COMMON_WHITE_WAVE_RING = @"白波の指輪";
        public const string COMMON_NEEDLE_FEATHER = @"ニードルフェザー";
        public const string COMMON_KOUSHITU_ORB = @"硬質のオーブ";
        public const string RARE_RED_ARM_BLADE = @"レッドアームブレード";
        public const string RARE_STRONG_SERPENT_CLAW = @"強靭なサーペントクロー";
        public const string RARE_STRONG_SERPENT_SHIELD = @"強靭なサーペントシールド";
        #endregion
        #region "後編、ガンツの武具屋３階"
        // 初版
        public const string COMMON_EXCELLENT_SWORD_3 = @"エクセレント・ソード【＋３】";
        public const string COMMON_EXCELLENT_KNUCKLE_3 = @"エクセレント・ナックル【＋３】";
        public const string COMMON_EXCELLENT_ROD_3 = @"エクセレント・ロッド【＋３】";
        public const string COMMON_EXCELLENT_BUSTER_3 = @"エクセレント・バスター【＋３】";
        public const string COMMON_EXCELLENT_ARMOR_3 = @"エクセレント・アーマー【＋３】";
        public const string COMMON_EXCELLENT_CROSS_3 = @"エクセレント・クロス【＋３】";
        public const string COMMON_EXCELLENT_ROBE_3 = @"エクセレント・ローブ【＋３】";
        public const string COMMON_EXCELLENT_SHIELD_3 = @"エクセレント・シールド【＋３】";
        public const string COMMON_WINTERS_HORN = @"ウィンターズ・ホーン";
        public const string RARE_CHILL_BONE_SHIELD = @"チル・ボーン・シールド";
        // 合成
        public const string COMMON_STEEL_BLADE = @"スチール・ブレード【＋４】";
        public const string COMMON_SNOW_GUARD = @"スノーガード";
        public const string COMMON_LIZARDSCALE_ARMOR = @"リザードスケイル・アーマー【＋４】";
        public const string COMMON_PENGUIN_OF_PENGUIN = @"ペンギン・オブ・ペンギン";
        public const string COMMON_ARGNIAN_TUNIC = @"アルゴニアン・チュニック";
        public const string COMMON_ANIMAL_FUR_CROSS = @"アニマル・ファークロス";
        public const string RARE_SPLASH_BARE_CLAW = @"スプラッシュ・ベアクロー";
        public const string EPIC_GATO_HAWL_OF_GREAT = @"ガトゥ・ハウル・オブ・グレイト";
        #endregion
        #region "後編、ガンツの武具屋４階"
        // 初版
        public const string COMMON_MASTER_SWORD_4 = @"マスター・ソード【＋４】";
        public const string COMMON_MASTER_KNUCKLE_4 = @"マスター・ナックル【＋４】";
        public const string COMMON_MASTER_ROD_4 = @"マスター・ロッド【＋４】";
        public const string COMMON_MASTER_AXE_4 = @"マスター・アックス【＋４】";
        public const string COMMON_MASTER_ARMOR_4 = @"マスター・アーマー【＋４】";
        public const string COMMON_MASTER_CROSS_4 = @"マスター・クロス【＋４】";
        public const string COMMON_MASTER_ROBE_4 = @"マスター・ローブ【＋４】";
        public const string COMMON_MASTER_SHIELD_4 = @"マスター・シールド【＋４】";
        public const string RARE_SUPERIOR_CHOSEN_ROD = @"スペリオル・チューズン・ロッド";
        public const string RARE_TYOU_KOU_SWORD = @"超硬の剣【＋６】";
        public const string RARE_TYOU_KOU_ARMOR = @"超硬の鎧【＋６】";
        public const string RARE_TYOU_KOU_SHIELD = @"超硬の盾【＋６】";
        public const string RARE_WHITE_GOLD_CROSS = @"ホワイト・ゴールド・クロス";
        // 合成
        public const string RARE_HUNTERS_EYE = @"ハンターズ・アイ";
        public const string RARE_ONEHUNDRED_BUTOUGI = @"百獣皮の舞踏衣";
        public const string RARE_DARKANGEL_CROSS = @"ダークエンジェル・クロス";
        public const string RARE_DEVIL_KILLER = @"デビル・キラー";
        public const string RARE_TRUERED_MASTER_BLADE = @"真紅炎・マスターブレイド";
        public const string RARE_VOID_HYMNSONIA = @"ヴォイド・ヒムソニア";
        public const string RARE_SEAL_OF_BALANCE = @"シール・オブ・バランス";
        public const string RARE_DOOMBRINGER = @"ドゥーム・ブリンガー";
        public const string EPIC_MEIKOU_DOOMBRINGER = @"冥光・ドゥームブリンガー";
        #endregion
        #region "後編、ガンツの武具屋５階"
        #endregion
        #region "真実世界"
        #endregion

        #region "後編、ラナのポーション店１階"
        // ポーション系 前編からあるのも混じっているが、後編ではここで宣言
        // 初版
        public const string POOR_SMALL_RED_POTION = @"小さい赤ポーション";
        public const string POOR_SMALL_BLUE_POTION = @"小さい青ポーション";
        public const string POOR_SMALL_GREEN_POTION = @"小さい緑ポーション";
        public const string POOR_POTION_CURE_POISON = @"解毒薬";
        public const string COMMON_REVIVE_POTION_MINI = @"リヴァイヴポーション・ミニ";
        public const string RARE_REVIVE_POTION = @"リヴァイヴポーション";
        
        // 合成
        public const string COMMON_POTION_NATURALIZE = @"ナチュラライズ・ポーション";
        public const string COMMON_POTION_RESIST_FIRE = @"耐熱ポーション";
        public const string COMMON_POTION_MAGIC_SEAL = @"マジック・シール薬";
        public const string COMMON_POTION_ATTACK_SEAL = @"アタック・シール薬";
        public const string COMMON_POTION_CURE_BLIND = @"キュア・ブラインド";
        public const string RARE_POTION_MOSSGREEN_DREAM = @"モスグリーン・ドリーム";
        public const string RARE_DRYAD_SAGE_POTION = @"ドライアドの秘薬";
        #endregion
        #region "後編、ラナのポーション店２階"
        // ２階
        // 初版
        public const string COMMON_NORMAL_RED_POTION = @"赤ポーション【通常】";
        public const string COMMON_NORMAL_BLUE_POTION = @"青ポーション【通常】";
        public const string COMMON_NORMAL_GREEN_POTION = @"緑ポーション【通常】";
        public const string COMMON_RESIST_POISON = @"耐解毒ポーション"; // ダンジョン２階進む所でラナより景品
        // 合成
        public const string COMMON_POTION_OVER_GROWTH = @"オーバー・グロース";
        public const string COMMON_POTION_RAINBOW_IMPACT = @"レインボー・インパクト";
        public const string COMMON_POTION_BLACK_GAST = @"ブラック・ガスト";
        public const string COMMON_SOUKAI_DRINK_SS = @"爽快ドリンク【Ｓ＆Ｓ】";
        public const string COMMON_TUUKAI_DRINK_DD = @"痛快ドリンク【Ｄ＆Ｄ】";
        #endregion
        #region "後編、ラナのポーション店３階"
        // ３階
        // 初版
        public const string COMMON_LARGE_RED_POTION = @"赤ポーション【大】";
        public const string COMMON_LARGE_BLUE_POTION = @"青ポーション【大】";
        public const string COMMON_LARGE_GREEN_POTION = @"緑ポーション【大】";
        // 合成
        public const string COMMON_FAIRY_BREATH = @"フェアリー・ブレス";
        public const string COMMON_HEART_ACCELERATION = @"ハート・アクセラレーション";
        public const string RARE_SAGE_POTION_MINI = @"賢者の秘薬【ミニ】";
        #endregion
        #region "後編、ラナのポーション店４階"
        // ４階
        // 初版
        public const string COMMON_HUGE_RED_POTION = @"赤ポーション【特大】";
        public const string COMMON_HUGE_BLUE_POTION = @"青ポーション【特大】";
        public const string COMMON_HUGE_GREEN_POTION = @"緑ポーション【特大】";
        // 合成
        public const string RARE_POWER_SURGE = @"パワー・サージ";
        public const string RARE_ELEMENTAL_SEAL = @"エレメンタル・シール";
        public const string RARE_GENSEI_MAGIC_BOTTLE = @"源正の魔力剤";
        public const string RARE_GENSEI_TAIMA_KUSURI = @"源正の退魔薬";
        public const string RARE_MIND_ILLUSION = @"マインド・イリュージョン";
        public const string RARE_SHINING_AETHER = @"シャイニング・エーテル";
        public const string RARE_ZETTAI_STAMINAUP = @"確約の体力増強剤";
        public const string RARE_BLACK_ELIXIR = @"ブラック・エリクシール";
        public const string RARE_ZEPHER_BREATH = @"ゼフィール・ブレス";
        public const string RARE_COLORLESS_ANTIDOTE = @"カラレス・アンチドーテ";

        #endregion
        #region "後編、ラナのポーション店５階"
        // ５階
        // 初版
        public const string COMMON_GORGEOUS_RED_POTION = @"赤ポーション【豪華版】";
        public const string COMMON_GORGEOUS_BLUE_POTION = @"青ポーション【豪華版】";
        public const string COMMON_GORGEOUS_GREEN_POTION = @"緑ポーション【豪華版】";
        #endregion
        #region "真実世界"
        public const string RARE_PERFECT_RED_POTION = @"赤ポーション【完全版】";
        public const string RARE_PERFECT_BLUE_POTION = @"青ポーション【完全版】";
        public const string RARE_PERFECT_GREEN_POTION = @"緑ポーション【完全版】";
        #endregion

        // 後編、ハンナの料理屋
        #region "食事"
        // １階
        public const string FOOD_KATUCARRY = @"激辛カツカレー定食";
        public const string FOOD_OLIVE_AND_ONION = @"オリーブパンとオニオンスープ";
        public const string FOOD_INAGO_AND_TAMAGO = @"イナゴの佃煮と卵和え定食";
        public const string FOOD_USAGI = @"ウサギ肉のシチュー";
        public const string FOOD_SANMA = @"サンマ定食（煮物添え）";
        // ２階
        public const string FOOD_FISH_GURATAN = @"フィッシュ・グラタン";
        public const string FOOD_SEA_TENPURA = @"海鮮サクサク天プラ";
        public const string FOOD_TRUTH_YAMINABE_1 = @"真実の闇鍋（パート１）";
        public const string FOOD_OSAKANA_ZINGISKAN = @"お魚ジンギスカン";
        public const string FOOD_RED_HOT_SPAGHETTI = @"レッドホット・スパゲッティ";
        // ３階
        public const string FOOD_HINYARI_YASAI = @"ヒンヤリ・カリっと野菜定食";
        public const string FOOD_AZARASI_SHIOYAKI = @"白アザラシの塩焼き";
        public const string FOOD_WINTER_BEEF_CURRY = @"ウィンター・ビーフカレー";
        public const string FOOD_GATTURI_GOZEN = @"ガッツリ骨太御膳";
        public const string FOOD_KOGOERU_DESSERT = @"身も凍える・ブルーデザート";
        // ４階
        public const string FOOD_BLACK_BUTTER_SPAGHETTI = @"ブラックバター・スパゲッティ";
        public const string FOOD_KOROKORO_PIENUS_HAMBURG = @"コロコロ・ピーナッツ・ハンバーグ";
        public const string FOOD_PIRIKARA_HATIMITSU_STEAK = @"ピリ辛・ハチミツステーキ定食";
        public const string FOOD_HUNWARI_ORANGE_TOAST = @"ふんわり・オレンジトースト";
        public const string FOOD_TRUTH_YAMINABE_2 = @"真実の闇鍋（パート２）";
        #endregion

        #endregion

        #region "画像ファイル"
        public const string IMAGE_DRAGON_BRIYARD = @"Dragon_Briyard.png";
        public const string IMAGE_DRAGON_DEEPSEA = @"Dragon_DeepSea.png";
        public const string IMAGE_DRAGON_AZOLD = @"Dragon_Azold.png";
        public const string IMAGE_DRAGON_ZEED = @"Dragon_Zeed.png";
        public const string IMAGE_DRAGON_ETULA = @"Dragon_Etula.png";
        #endregion

        #region "フォントファイル"
        public const string FONT_KOUZAN_MOUHITSU = @"KouzanMouhituFontOTF";
        #endregion

        #region "効果音データファイル名"
        public const string SOUND_FIREBALL = @"FireBall";
        public const string SOUND_ICENEEDLE = @"IceNeedle";
        public const string SOUND_ENEMY_ATTACK1 = @"EnemyAttack1";
        public const string SOUND_SWORD_SLASH1 = @"SwordSlash1";
        public const string SOUND_STRAIGHT_SMASH = @"StraightSmash";
        public const string SOUND_FRESH_HEAL = @"FreshHeal";
        public const string SOUND_CELESTIAL_NOVA = @"CelestialNova";
        public const string SOUND_MAGIC_ATTACK = @"MagicAttack";
        public const string SOUND_KINETIC_SMASH = @"KineticSmash";
        public const string SOUND_ARCANE_DESTRUCTION = @"KineticSmash";
        public const string SOUND_CRUSHING_BLOW = @"CrushingBlow";
        public const string SOUND_SOUL_INFINITY = @"Catastrophe";
        public const string SOUND_CATASTROPHE = @"Catastrophe";
        public const string SOUND_OBORO_IMPACT = @"Catastrophe";
        public const string SOUND_ABYSS_EYE = @"WhiteOut";
        public const string SOUND_DARK_BLAST = @"DarkBlast";
        public const string SOUND_DOOM_BLADE = @"DarkBlast";
        public const string SOUND_PHANTASMAL_WIND = @"HeatBoost";
        public const string SOUND_PARADOX_IMAGE = @"RiseOfImage";
        public const string SOUND_PIERCING_FLAME = @"FireBall";
        public const string SOUND_DEMONIC_IGNITE = @"FireBall";
        public const string SOUND_VORTEX_FIELD = @"DispelMagic";
        public const string SOUND_GLORY = @"Glory";
        public const string SOUND_STATIC_BARRIER = @"Glory";
        public const string SOUND_NOURISH_SENSE = @"WordOfLife";
        public const string SOUND_LIFE_TAP = @"LifeTap";
        public const string SOUND_SYUTYU_DANZETSU = @"NothingOfNothingness";
        public const string SOUND_JUNKAN_SEIYAKU = @"NothingOfNothingness";
        public const string SOUND_ORA_ORA_ORAAA = @"Catastrophe";
        public const string SOUND_SHINZITSU_HAKAI = @"Catastrophe";
        public const string SOUND_HYMN_CONTRACT = @"Resurrection";
        public const string SOUND_ENDLESS_ANTHEM = @"Resurrection";
        public const string SOUND_FLAME_STRIKE = @"FlameStrike";
        public const string SOUND_SIGIL_OF_HOMURA = @"FlameStrike";
        public const string SOUND_AUSTERITY_MATRIX = @"OneImmunity";
        public const string SOUND_RED_DRAGON_WILL = @"StraightSmash";
        public const string SOUND_BLUE_DRAGON_WILL = @"StraightSmash";
        public const string SOUND_ECLIPSE_END = @"BlackContract";
        public const string SOUND_SIN_FORTUNE = @"BlackContract";
        public const string SOUND_BLACK_FLARE = @"BlackContract";
        public const string SOUND_DEATH_DENY = @"BlackContract";
        public const string SOUND_DEATH = @"BlackContract";
        public const string SOUND_RISINGKNUCKLE = @"RisingKnuckle";
        public const string SOUND_DAMNATION = @"Damnation";
        public const string SOUND_CHOSEN_SACRIFY = @"Damnation";
        public const string SOUND_ABSOLUTE_ZERO = @"AbsoluteZero";
        public const string SOUND_LAVA_ANNIHILATION = @"LavaAnnihilation";
        public const string SOUND_KOKUEN_BLUE_EXPLODE = @"LavaAnnihilation";
        public const string SOUND_VOLCANICWAVE = @"VolcanicWave";
        public const string SOUND_MEGID_BLAZE = @"VolcanicWave";
        public const string SOUND_FROZENLANCE = @"FrozenLance";
        public const string SOUND_SHARPNEL_NEEDLE = @"FrozenLance";
        public const string SOUND_WHITEOUT = @"Whiteout";
        public const string SOUND_WORD_OF_POWER = @"WordOfPower";
        public const string SOUND_TIME_STOP = @"TimeStop";
        public const string SOUND_WARP_GATE = @"TimeStop";
        public const string SOUND_GENESIS = @"Genesis";
        public const string SOUND_STANCE_OF_DOUBLE = @"Genesis";
        public const string SOUND_ZETA_EXPLOSION = @"LavaAnnihilation";

        public const string SOUND_GET_EPIC_ITEM = @"GetEpicItem";
        public const string SOUND_GET_RARE_ITEM = @"GetRareItem";

        public const string SOUND_LVUP_FELTUS = @"LvupFeltus";

        public const string SOUND_WALL_HIT = @"WallHit";
        public const string SOUND_FOOT_STEP = @"footstep";
        public const string SOUND_REST_INN = @"RestInn";
        public const string SOUND_DEVOURING_PLAGUE = @"DevouringPlague";

        public const string SOUND_LEVEL_UP = @"LvUp";

        // ここから下はサウンドファイル名称を直接記述したものをナンバリング
        public const string SOUND_1 = @"AbsoluteZero";
        public const string SOUND_2 = @"AbsorbWater";
        public const string SOUND_3 = @"AeroBlade";
        public const string SOUND_4 = @"AetherDrive";
        public const string SOUND_5 = @"AntiStun";
        public const string SOUND_6 = @"BlackContract";
        public const string SOUND_7 = @"BloodyVengeance";
        public const string SOUND_8 = @"BlueLightning";
        public const string SOUND_9 = @"Catastrophe";
        public const string SOUND_10 = @"CelestialNova";
        public const string SOUND_11 = @"Cleansing";
        public const string SOUND_12 = @"CrushingBlow";
        public const string SOUND_13 = @"Damnation";
        public const string SOUND_14 = @"DarkBlast";
        public const string SOUND_15 = @"Deflection";
        public const string SOUND_17 = @"DispelMagic";
        public const string SOUND_18 = @"EnemyAttack1";
        public const string SOUND_19 = @"EternalPresence";
        public const string SOUND_20 = @"FireBall";
        public const string SOUND_21 = @"FlameAura";
        public const string SOUND_22 = @"FlameStrike";
        public const string SOUND_23 = @"footstep";
        public const string SOUND_24 = @"FreshHeal";
        public const string SOUND_25 = @"FrozenLance";
        public const string SOUND_26 = @"GaleWind";
        public const string SOUND_27 = @"Genesis";
        public const string SOUND_28 = @"GetEpicItem";
        public const string SOUND_29 = @"GetRareItem";
        public const string SOUND_30 = @"Glory";
        public const string SOUND_31 = @"HeatBoost";
        public const string SOUND_32 = @"HighEmotionality";
        public const string SOUND_33 = @"Hit01";
        public const string SOUND_34 = @"HolyShock";
        public const string SOUND_35 = @"IceNeedle";
        public const string SOUND_36 = @"ImmortalRave";
        public const string SOUND_37 = @"InnerInspiration";
        public const string SOUND_38 = @"KineticSmash";
        public const string SOUND_39 = @"LavaAnnihilation";
        public const string SOUND_40 = @"LifeTap";
        public const string SOUND_41 = @"LvUp";
        public const string SOUND_42 = @"LvupFeltus";
        public const string SOUND_43 = @"MagicAttack";
        public const string SOUND_44 = @"MirrorImage";
        public const string SOUND_45 = @"NothingOfNothingness";
        public const string SOUND_46 = @"OneImmunity";
        public const string SOUND_47 = @"PainfulInsanity";
        public const string SOUND_48 = @"PromisedKnowledge";
        public const string SOUND_49 = @"Protection";
        public const string SOUND_50 = @"PutiFireBall";
        public const string SOUND_52 = @"Resurrection";
        public const string SOUND_53 = @"RiseOfImage";
        public const string SOUND_54 = @"RisingKnuckle";
        public const string SOUND_55 = @"SaintPower";
        public const string SOUND_56 = @"ShadowPact";
        public const string SOUND_57 = @"SpecialHit";
        public const string SOUND_58 = @"StanceOfDeath";
        public const string SOUND_59 = @"StanceOfFlow";
        public const string SOUND_60 = @"StraightSmash";
        public const string SOUND_61 = @"SwordSlash1";
        public const string SOUND_62 = @"TimeStop";
        public const string SOUND_63 = @"Tranquility";
        public const string SOUND_64 = @"TruthVision";
        public const string SOUND_65 = @"VoidExtraction";
        public const string SOUND_66 = @"VolcanicWave";
        public const string SOUND_67 = @"WallHit";
        public const string SOUND_68 = @"WhiteOut";
        public const string SOUND_69 = @"WordOfFortune";
        public const string SOUND_70 = @"WordOfLife";
        public const string SOUND_71 = @"WordOfPower";

        public const string SOUND_OBJECTIVE_ADD = "ObjectiveAdd";
        public const string SOUND_OBJECTIVE_COMP = "ObjectiveComplete";

        public const string SOUND_MQ_BEGIN = "MonsterQuestBegin";
        public const string SOUND_MQ_REWARD = "MonsterQuestReward";
        #endregion

        #region "敵の名前"
        #region "ダンジョン１階"
        public const string ENEMY_HIYOWA_BEATLE = @"ひ弱なビートル";
        public const string ENEMY_HENSYOKU_PLANT = @"変色したプラント";
        public const string ENEMY_GREEN_CHILD = @"グリーン・チャイルド";
        public const string ENEMY_TINY_MANTIS = @"タイニー・マンティス";
        public const string ENEMY_KOUKAKU_WURM = @"甲殻ワーム";
        public const string ENEMY_MANDRAGORA = @"マンドラゴラ";

        public const string ENEMY_SUN_FLOWER = @"サン・フラワー";
        public const string ENEMY_RED_HOPPER = @"レッド・ホッパー";
        public const string ENEMY_EARTH_SPIDER = @"アースパイダー";
        public const string ENEMY_WILD_ANT = @"ワイルド・アント";
        public const string ENEMY_ALRAUNE = @"アルラウネ";
        public const string ENEMY_POISON_MARY = @"ポイズン・マリー";

        public const string ENEMY_SPEEDY_TAKA = @"俊敏な鷹";
        public const string ENEMY_ZASSYOKU_RABBIT = @"雑食ウサギ";
        public const string ENEMY_WONDER_SEED = @"ワンダー・シード";
        public const string ENEMY_ASH_CREEPER = @"アッシュ・クリーパー";
        public const string ENEMY_GIANT_SNAKE = @"ジャイアント・スネーク";
        public const string ENEMY_FLANSIS_KNIGHT = @"フランシス・ナイト";
        public const string ENEMY_SHOTGUN_HYUI = @"ショットガン・ヒューイ";

        public const string ENEMY_WAR_WOLF = @"番狼";
        public const string ENEMY_BRILLIANT_BUTTERFLY = @"ブリリアント・バタフライ";
        public const string ENEMY_MIST_ELEMENTAL = @"ミスト・エレメンタル";
        public const string ENEMY_WHISPER_DRYAD = @"ウィスパー・ドライアド";
        public const string ENEMY_BLOOD_MOSS = @"ブラッド・モス";
        public const string ENEMY_MOSSGREEN_DADDY = @"モスグリーン・ダディ";

        public const string ENEMY_BOSS_KARAMITUKU_FLANSIS = @"一階の守護者：絡みつくフランシス";

        public const string ENEMY_DRAGON_SOKUBAKU_BRIYARD = @"束縛する者ブライヤード(The Restrainer)";
        #endregion
        #region "ダンジョン２階"

        public const string ENEMY_DAGGER_FISH = @"ダガーフィッシュ";
        public const string ENEMY_SIPPU_FLYING_FISH = @"疾風・フライングフィッシュ";
        public const string ENEMY_ORB_SHELLFISH = @"オーブ・シェルフィッシュ";
        public const string ENEMY_SPLASH_KURIONE = @"スプラッシュ・クリオネ";
        public const string ENEMY_TRANSPARENT_UMIUSHI = @"透明なウミウシ";

        public const string ENEMY_ROLLING_MAGURO = @"ローリング・マグロ";
        public const string ENEMY_RANBOU_SEA_ARTINE = @"乱暴なシー・アーチン";
        public const string ENEMY_BLUE_SEA_WASI = @"青海鷲";
        public const string ENEMY_BRIGHT_SQUID = @"ブライト・スクイッド";
        public const string ENEMY_GANGAME = @"頑亀";
        public const string ENEMY_BIGMOUSE_JOE = @"ビッグマウス・ジョー";

        public const string ENEMY_MOGURU_MANTA = @"モーグル・マンタ";
        public const string ENEMY_FLOATING_GOLD_FISH = @"浮遊するゴールドフィッシュ";
        public const string ENEMY_GOEI_HERMIT_CLUB = @"護衛隊・ハーミットクラブ";
        public const string ENEMY_ABARE_SHARK = @"暴れ大ザメ";
        public const string ENEMY_VANISHING_CORAL = @"バニッシング・コーラル";
        public const string ENEMY_CASSY_CANCER = @"キャシー・ザ・キャンサー";

        public const string ENEMY_BLACK_STARFISH = @"ブラック・スターフィッシュ";
        public const string ENEMY_RAINBOW_ANEMONE = @"レインボー・アネモネ";
        public const string ENEMY_MACHIBUSE_ANKOU = @"待ち伏せアンコウ";
        public const string ENEMY_EDGED_HIGH_SHARK = @"エッジド・ハイ・シャーク";
        public const string ENEMY_EIGHT_EIGHT = @"エイト・エイト";

        public const string ENEMY_BRILLIANT_SEA_PRINCE = @"輝ける海の王子";
        public const string ENEMY_ORIGIN_STAR_CORAL_QUEEN = @"源星・珊瑚の女王";
        public const string ENEMY_SHELL_SWORD_KNIGHT = @"シェル・ザ・ソードナイト";
        public const string ENEMY_JELLY_EYE_BRIGHT_RED = @"ジェリーアイ・熱光";
        public const string ENEMY_JELLY_EYE_DEEP_BLUE = @"ジェリーアイ・流冷";
        public const string ENEMY_SEA_STAR_KNIGHT_AEGIRU = @"海星騎士・エーギル";
        public const string ENEMY_SEA_STAR_KNIGHT_AMARA = @"海星騎士・アマラ";
        public const string ENEMY_SEA_STAR_ORIGIN_KING = @"海星源の王";

        public const string ENEMY_BOSS_LEVIATHAN = @"二階の守護者：大海蛇リヴィアサン";

        public const string ENEMY_DRAGON_TINKOU_DEEPSEA = @"沈降せし者ディープシー";//(The Akashic)";
        #endregion
        #region "ダンジョン３階"
        public const string ENEMY_TOSSIN_ORC = @"突進オーク";
        public const string ENEMY_SNOW_CAT = @"スノー・キャット";
        public const string ENEMY_WAR_MAMMOTH = @"ウォー・マンモス";
        public const string ENEMY_WINGED_COLD_FAIRY = @"ウィングド・コールドフェアリー";
        public const string ENEMY_FREEZING_GRIFFIN = @"フリージング・グリフィン";

        public const string ENEMY_BRUTAL_OGRE = @"ブルータル・オーガ";
        public const string ENEMY_HYDRO_LIZARD = @"ハイドロ・リザード";
        public const string ENEMY_PENGUIN_STAR = @"ペンギンスター";
        public const string ENEMY_ICEBERG_SPIRIT = @"アイスバーグ・スピリット";
        public const string ENEMY_SWORD_TOOTH_TIGER = @"剣歯虎";
        public const string ENEMY_FEROCIOUS_RAGE_BEAR = @"フェロシアス・レイジベア";

        public const string ENEMY_WINTER_ORB = @"ウィンター・オーヴ";
        public const string ENEMY_PATHFINDING_LIGHTNING_AZARASI = @"追従する雷アザラシ";
        public const string ENEMY_MAJESTIC_CENTAURUS = @"マジェスティック・ケンタウルス";
        public const string ENEMY_INTELLIGENCE_ARGONIAN = @"知的なアルゴニアン";
        public const string ENEMY_MAGIC_HYOU_RIFLE = @"魔法雹穴銃";
        public const string ENEMY_PURE_BLIZZARD_CRYSTAL = @"ピュア・ブリザード・クリスタル";

        public const string ENEMY_PURPLE_EYE_WARE_WOLF = @"紫目・ウェアウルフ";
        public const string ENEMY_FROST_HEART = @"フロスト・ハート";
        public const string ENEMY_WHITENIGHT_GRIZZLY = @"白夜のグリズリー";
        public const string ENEMY_WIND_BREAKER = @"ウィンド・ブレイカー";
        public const string ENEMY_TUNDRA_LONGHORN_DEER = @"ツンドラ・ロングホーン・ディア";

        public const string ENEMY_BOSS_HOWLING_SEIZER = @"恐鳴主ハウリング・シーザー";

        public const string ENEMY_DRAGON_DESOLATOR_AZOLD = @"凍てつく者アゾルド";//(The Desolate)";
        #endregion
        #region "ダンジョン４階"
        public const string ENEMY_GENAN_HUNTER = @"幻暗ハンター";
        public const string ENEMY_BEAST_MASTER = @"ビーストマスター";
        public const string ENEMY_ELDER_ASSASSIN = @"エルダーアサシン";
        public const string ENEMY_FALLEN_SEEKER = @"フォールン・シーカー";
        public const string ENEMY_MEPHISTO_RIGHTARM = @"メフィスト・ザ・ライトアーム";

        public const string ENEMY_DARK_MESSENGER = @"闇の眷属";
        public const string ENEMY_MASTER_LOAD = @"マスターロード";
        public const string ENEMY_EXECUTIONER = @"エグゼキューショナー";
        public const string ENEMY_MARIONETTE_NEMESIS = @"マリオネット・ネメシス";
        public const string ENEMY_BLACKFIRE_MASTER_BLADE = @"黒炎マスターブレイド";
        public const string ENEMY_SIN_THE_DARKELF = @"シン・ザ・ダークエルフ";

        public const string ENEMY_SUN_STRIDER = @"サン・ストライダー";
        public const string ENEMY_ARC_DEMON = @"アークデーモン";
        public const string ENEMY_BALANCE_IDLE = @"天秤を司る者";
        public const string ENEMY_UNDEAD_WYVERN = @"アンデッド・ワイバーン";
        public const string ENEMY_GO_FLAME_SLASHER = @"業・フレイムスラッシャー";
        public const string ENEMY_DEVIL_CHILDREN = @"デビル・チルドレン";

        public const string ENEMY_HOWLING_HORROR = @"ハウリングホラー";
        public const string ENEMY_PAIN_ANGEL = @"ペインエンジェル";
        public const string ENEMY_CHAOS_WARDEN = @"カオス・ワーデン";
        public const string ENEMY_DREAD_KNIGHT = @"ドレッド・ナイト";
        public const string ENEMY_DOOM_BRINGER = @"ドゥームブリンガー";

        public const string ENEMY_BOSS_LEGIN_ARZE = @"闇焔レギィン・アーゼ";
        public const string ENEMY_BOSS_LEGIN_ARZE_1 = @"闇焔レギィン・アーゼ【瘴気】";
        public const string ENEMY_BOSS_LEGIN_ARZE_2 = @"闇焔レギィン・アーゼ【無音】";
        public const string ENEMY_BOSS_LEGIN_ARZE_3 = @"闇焔レギィン・アーゼ【深淵】";

        public const string ENEMY_DRAGON_IDEA_CAGE_ZEED = @"黙考せし者ジード";
        #endregion
        #region "ダンジョン５階"
        public const string ENEMY_PHOENIX = @"Phoenix";
        public const string ENEMY_NINE_TAIL = @"Nine Tail";
        public const string ENEMY_MEPHISTOPHELES = @"Mephistopheles";
        public const string ENEMY_JUDGEMENT = @"Judgement";
        public const string ENEMY_EMERALD_DRAGON = @"Emerald Dragon";

        public const string ENEMY_BOSS_BYSTANDER_EMPTINESS = @"支　配　竜";
        public const string ENEMY_DRAGON_ALAKH_VES_T_ETULA = @"AlakhVes T Etula";

        #endregion
        #region "真実世界"
        public const string ENEMY_LAST_RANA_AMILIA = @"ラナ・アミリア "; // DUEL名識別のため、最後の空白スペースで意図的に区別
        public const string ENEMY_LAST_OL_LANDIS = @"オル・ランディス "; // DUEL名識別のため、最後の空白スペースで意図的に区別
        public const string ENEMY_LAST_SINIKIA_KAHLHANZ = @"シニキア・カールハンツ "; // DUEL名識別のため、最後の空白スペースで意図的に区別
        public const string ENEMY_LAST_VERZE_ARTIE = @"ヴェルゼ・アーティ "; // DUEL名識別のため、最後の空白スペースで意図的に区別
        public const string ENEMY_LAST_SIN_VERZE_ARTIE = @"【原罪】ヴェルゼ・アーティ";
        #endregion
        #region "Duel闘技場"
        public const string DUEL_SIN_OSCURETE = @"シン・オスキュレーテ"; // 60
        public const string DUEL_SIN_OSCURETE_DB = @"sin_oscurete";
        public const string DUEL_LADA_MYSTORUS = @"ラダ・ミストゥルス"; // 58
        public const string DUEL_LADA_MYSTORUS_DB = @"lada_mystorus";
        public const string DUEL_OHRYU_GENMA = @"オウリュウ・ゲンマ"; // 56
        public const string DUEL_OHRYU_GENMA_DB = @"ohryu_genma";
        public const string DUEL_VAN_HEHGUSTEL = @"ヴァン・ヘーグステル"; // 54
        public const string DUEL_VAN_HEHGUSTEL_DB = @"van_hehgustel";
        public const string DUEL_RVEL_ZELKIS = @"ルベル・ゼルキス"; // 52
        public const string DUEL_RVEL_ZELKIS_DB = @"rvel_zelkis";

        public const string DUEL_SHUVALTZ_FLORE = @"シュヴァルツェ・フローレ"; // 50
        public const string DUEL_SHUVALTZ_FLORE_DB = @"shuvaltz_flore";
        public const string DUEL_SUN_YU = @"サン・ユウ"; // 47
        public const string DUEL_SUN_YU_DB = @"sun_yu";
        public const string DUEL_CALMANS_OHN = @"カルマンズ・オーン"; // 44
        public const string DUEL_CALMANS_OHN_DB = @"calmans_ohn";
        public const string DUEL_ANNA_HAMILTON = @"アンナ・ハミルトン"; // 41
        public const string DUEL_ANNA_HAMILTON_DB = @"anna_hamilton";
        public const string DUEL_BILLY_RAKI = @"ビリー・ラキ"; // 38
        public const string DUEL_BILLY_RAKI_DB = @"billy_raki";

        public const string DUEL_KILT_JORJU = @"キルト・ジョルジュ"; // 35
        public const string DUEL_KILT_JORJU_DB = @"kilt_jorju";
        public const string DUEL_PERMA_WARAMY = @"ペルマ・ワラミィ"; // 32
        public const string DUEL_PERMA_WARAMY_DB = @"perma_waramy";
        public const string DUEL_SCOTY_ZALGE = @"スコーティ・ザルゲ"; // 29
        public const string DUEL_SCOTY_ZALGE_DB = @"scoty_zalge";
        public const string DUEL_LENE_COLTOS = @"レネ・コルトス"; // 26
        public const string DUEL_LENE_COLTOS_DB = @"lene_coltos";
        public const string DUEL_ADEL_BRIGANDY = @"アデル・ブリガンディ"; // 23
        public const string DUEL_ADEL_BRIGANDY_DB = @"adel_brigandy";

        public const string DUEL_SINIKIA_VEILHANZ = @"シニキア・ヴェイルハンツ"; // 20
        public const string DUEL_SINIKIA_VEILHANZ_DB = @"sinikia_veilhanz";
        public const string DUEL_JEDA_ARUS = @"ジェダ・アルス"; // 16
        public const string DUEL_JEDA_ARUS_DB = @"jeda_arus";
        public const string DUEL_KARTIN_MAI = @"カーティン・マイ"; // 13
        public const string DUEL_KARTIN_MAI_DB = @"kartin_mai";
        public const string DUEL_SELMOI_RO = @"セルモイ・ロウ"; // 10
        public const string DUEL_SELMOI_RO_DB = @"selmoi_ro";
        public const string DUEL_MAGI_ZELKIS = @"マーギ・ゼルキス"; // 7
        public const string DUEL_MAGI_ZELKIS_DB = @"magi_zelkis";
        public const string DUEL_EONE_FULNEA = @"エオネ・フルネア"; // 4
        public const string DUEL_EONE_FULNEA_DB = @"eone_fulnea";
        #endregion
        public const string DUEL_DUMMY_SUBURI = @"ダミー素振り君";

        // Duelist達の装備
        public const string EPIC_LADA_ACHROMATIC_ORB = @"ラダ・アクロマティック・オーブ";
        public const string COMMON_SWORD_OF_RVEL = @"ルベルの大剣";
        public const string COMMON_ARMOR_OF_RVEL = @"ルベルの鎧";

        public const string COMMON_ZELKIS_SWORD = @"ゼルキスの剣";
        public const string COMMON_ZELKIS_ARMOR = @"ゼルキスの鎧";
        public const string COMMON_WHITE_ROD = @"ホワイト・ロッド";
        public const string COMMON_BLUE_ROBE = @"青いローブ";
        public const string COMMON_FROZEN_BALL = @"凍結の玉";
        public const string RARE_PURE_GREEN_WATER = @"活湧水"; // Duel、ジェダ・アルスの持ち物だが、将来プレイヤーにも入手可能にする。
        public const string EPIC_DEVIL_EYE_ROD = @"Rod of D-Eye";
        public const string COMMON_ZALGE_CLAW = @"ザルゲの爪";
        public const string EPIC_FAZIL_ORB_1 = @"ファージルの宝珠【厳正】";
        public const string EPIC_FAZIL_ORB_2 = @"ファージルの宝珠【創授】";

        public const string EPIC_SHUVALTZ_FLORE_SWORD = @"壱なる舞踏剣";
        public const string EPIC_SHUVALTZ_FLORE_SHIELD = @"対なる無明剣";
        public const string EPIC_SHUVALTZ_FLORE_ARMOR = @"聡斎の黒装束";
        public const string EPIC_SHUVALTZ_FLORE_ACCESSORY1 = @"ファージルの宝珠【無限浄】";
        public const string EPIC_SHUVALTZ_FLORE_ACCESSORY2 = @"ファージルの宝珠【永循環】";

        // 元核習得時のシニキア・カールハンツ
        public const string DUEL_SINIKIA_KAHLHANZ = @"シニキア・カールハンツ";
        public const string LEGENDARY_DARKMAGIC_DEVIL_EYE = @"魔導デビルアイ";
        public const string LEGENDARY_DARKMAGIC_DEVIL_EYE_REPLICA = @"魔導デビルアイ（レプリカ）";
        public const string EPIC_YAMITUYUKUSA_MOON_ROBE = @"闇露草の円月衣";
        public const string LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING = @"Zveldose the Devil Fire Ring";
        public const string LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING = @"Anastelisa the Innocent Fire Ring";

        // 最下層ホログラムのシニキア・カールハンツ
        public const string EPIC_DARKMAGIC_DEVIL_EYE_2 = @"魔導デビルアイ(2ND-Ed)";
        public const string EPIC_YAMITUYUKUSA_MOON_ROBE_2 = @"闇露草 円月の衣";
        public const string LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING_2 = @"Zveldose the Devil-Fire Ring";
        public const string LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2 = @"Anastelisa the Innocent-Fire Ring";

        // オル・ランディス、仲間にする前
        public const string DUEL_OL_LANDIS = @"オル・ランディス";
        public const string LEGENDARY_GOD_FIRE_GLOVE = @"炎神グローブ";
        public const string POOR_GOD_FIRE_GLOVE_REPLICA = @"炎神グローブ（レプリカ）";
        public const string COMMON_AURA_ARMOR = @"オーラ・アーマー";
        public const string EPIC_AURA_ARMOR_OMEGA = @"永魂：エターナル・オーラアーマー";
        public const string COMMON_FATE_RING = @"フェイト・リング";
        public const string EPIC_FATE_RING_OMEGA = @"永絆：エターナル・フェイトリング";
        public const string COMMON_LOYAL_RING = @"ロイヤル・リング";
        public const string EPIC_LOYAL_RING_OMEGA = @"永正：エターナル・ロイヤルリング";

        // ヴェルゼ・アーティ、仲間にする前
        public const string LEGENDARY_TAU_WHITE_SILVER_SWORD_0 = @"[τ] White Silver Sword0";
        public const string LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR_0 = @"[λ] Black Aerial Armor0";
        public const string LEGENDARY_EPSIRON_HEAVENLY_SKY_WING_0 = @"[ε] Heavenly Sky Wing0";

        // ヴェルゼ・アーティ仲間にする時
        public const string RARE_WHITE_SILVER_SWORD_REPLICA = @"白銀の剣(レプリカ)";
        public const string RARE_BLACK_AERIAL_ARMOR_REPLICA = @"黒真空の鎧(レプリカ)";
        public const string RARE_HEAVENLY_SKY_WING_REPLICA = @"天空の翼(レプリカ)";

        // 最下層ホログラムのヴェルゼ・アーティ
        public const string EPIC_WHITE_SILVER_SWORD_REPLICA = @"【τ】 白銀の剣";
        public const string EPIC_BLACK_AERIAL_ARMOR_REPLICA = @"【λ】 黒真空の鎧";
        public const string EPIC_HEAVENLY_SKY_WING_REPLICA = @"【ε】 天空の翼";

        // ヴェルゼ・アーティ最終装備
        public const string LEGENDARY_TAU_WHITE_SILVER_SWORD = @"[τ] White Silver Sword";
        public const string EPIC_COLORLESS_ETERNAL_BREAKER = @"【新相】エターナル・ブレイカー";
        public const string LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR = @"[λ] Black Aerial Armor";
        public const string LEGENDARY_EPSIRON_HEAVENLY_SKY_WING = @"[ε] Heavenly Sky Wing";
        public const string LEGENDARY_SEFINE_HYMNUS_RING = @"【永遠】セフィーネの腕輪";

        // ダンジョン２階進む所でガンツより景品
        public const string POOR_PRACTICE_SWORD_ZERO = @"練習用の剣【封】";
        public const string POOR_PRACTICE_SWORD_1 = @"練習用の剣【Lv1】";
        public const string POOR_PRACTICE_SWORD_2 = @"練習用の剣【Lv2】";
        public const string COMMON_PRACTICE_SWORD_3 = @"練習用の剣【Lv3】";
        public const string COMMON_PRACTICE_SWORD_4 = @"練習用の剣【Lv4】";
        public const string RARE_PRACTICE_SWORD_5 = @"練習用の剣【Lv5】";
        public const string RARE_PRACTICE_SWORD_6 = @"練習用の剣【Lv6】";
        public const string EPIC_PRACTICE_SWORD_7 = @"練習用の剣【Lv7】";
        public const string LEGENDARY_FELTUS = @"神剣  フェルトゥーシュ";
        #endregion

        #region "朝・夕方・夜、ファージル宮殿裏"
        public const string BACKGROUND_MORNING = @"hometown"; // ".jpg"; // change unity
        public const string BACKGROUND_EVENING = @"hometown_evening"; // ".jpg"; // change unity
        public const string BACKGROUND_NIGHT = @"hometown2"; // ".jpg"; // change unity
        public const string BACKGROUND_SECRETFIELD_OF_FAZIL = @"SecretFieldOfFazil"; // ".jpg"; // change unity
        public const string BACKGROUND_FIELD_OF_FIRSTPLACE = @"Field1"; // ".jpg"; // change unity
        public const string BACKGROUND_FAZIL_CASTLE = @"FazilCastle"; // ".jpg"; // change unity
        #endregion

        public const string OK_BUTTON_IMAGE = @"OkButton";// ".jpg"; // change unity
        public const string OK_BUTTON_IMAGE_BLACK = @"OkButton_Black";// ".jpg"; // change unity

        public const string DUNGEON_BACKGROUND = @"background3";// ".jpg"; // change unity

        public const string WE2_FILE = @"TruthWorldEnvironment.xml";
        public const string WE3_FILE = @"SingleWorldEnvironment.xml";
        #endregion

        // BUFF追加向けファイル名
        public const string FLASH_BLAZE_BUFF = @"FlashBlaze_Buff"; // 炎追加効果
        public const string AFTER_REVIVE_HALF = @"AfterReviveHalf";
        public const string FIRE_DAMAGE_2 = @"FireDamage2";
        public const string BLACK_MAGIC = @"BlackMagic";
        public const string CHAOS_DESPERATE = @"ChaosDesperate";
        public const string ICHINARU_HOMURA = @"IchinaruHomura";
        public const string ABYSS_FIRE = @"AbyssFire";
        public const string LIGHT_AND_SHADOW = @"LightAndShadow";
        public const string ETERNAL_DROPLET = @"EternalDroplet";
        public const string AUSTERITY_MATRIX_OMEGA = @"AusterityMatrixOmega";
        public const string VOICE_OF_ABYSS = @"VoiceOfAbyss";
        public const string ABYSS_WILL = @"AbyssWill";
        public const string THE_ABYSS_WALL = @"TheAbyssWall";

        // レギィンアーゼマナコスト
        public const int COST_ICHINARU_HOMURA = 35000;
        public const int COST_ABYSS_FIRE = 32000;
        public const int COST_LIGHT_AND_SHADOW = 60000;
        public const int COST_ETERNAL_DROPLET = 48000;
        public const int COST_AUSTERITY_MATRIX_OMEGA = 95000;
        public const int COST_VOICE_OF_ABYSS = 87000;
        public const int COST_ABYSS_WILL = 25000;
        public const int COST_THE_ABYSS_WALL = 100000;

        // 最終戦[原罪]ヴェルゼ・アーティ
        public const string FINAL_ADEST_ESPELANTIE = @"AdestEspelantie";
        public const string FINAL_INVISIBLE_HUNDRED_CUTTER = @"InvisibleHundredCutter";
        public const string FINAL_ZERO_INNOCENT_SIN = @"ZeroInnocentSin";
        public const string FINAL_LADARYNTE_CHAOTIC_SCHEMA = @"LadarynteChaoticSchema";
        public const string FINAL_SEFINE_PAINFUL_HYMNUS = @"SefinePainfulHymnus";
        public const string FINAL_PERFECT_FALSE_DIMENSION = @"PerfectFalseDimension";

        // 最終戦ライフカウント
        public const string LIFE_COUNT = @"LifePoint";
        public const string BUFF_LIFE_COUNT = @"生命";
        public const string CHAOTIC_SCHEMA = @"ChaoticSchema";
        public const string BUFF_CHAOTIC_SCHEMA = @"カオス分身";

        // 素材判別文字
        public const string DESCRIPTION_SELL_ONLY = @"【売却専用品】" + "\r\n";
        public const string DESCRIPTION_EQUIP_MATERIAL = @"【武具素材】" + "\r\n";
        public const string DESCRIPTION_POTION_MATERIAL = @"【ポーション素材】" + "\r\n";
        public const string DESCRIPTION_FOOD_MATERIAL = @"【食材】" + "\r\n";
        public const string DESCRIPTION_WEAPON = @"【武器】";
        public const string DESCRIPTION_SHIELD = @"【盾】";
        public const string DESCRIPTION_ARMOR = @"【防具】";
        public const string DESCRIPTION_ACCESSORY = @"【アクセサリ】";
        public const string DESCRIPTION_POTION = @"【消耗品】";
        public const string DESCRIPTION_BLUEORB = @"【専用品】";

        public const string MUGEN_LOOP = @"９８３２６";

        public const string VINSGALDE = @"ヴィンスガルデ";
        public const string ORGAWEIN = @"オルガウェイン傭兵訓練施設";
        
        // 画面名称(add unity)
        public const string Title = @"Title";
        public const string Tutorial = @"Tutorial";
        public const string GameSetting = @"GameSetting";
		public const string DungeonTicket = @"DungeonTicket";
        public const string TruthAnswer = @"TruthAnswer";
        public const string TruthBattleEnemy = @"TruthBattleEnemy";
        public const string TruthBattleSetting = @"TruthBattleSetting";
        public const string TruthChoiceStatue = @"TruthChoiceStatue";
        public const string TruthChooseCommand = @"TruthChooseCommand";
        public const string TruthDecision = @"TruthDecision";
        public const string TruthDecision2 = @"TruthDecision2";
        public const string TruthDecision3 = @"TruthDecision3";
        public const string TruthDuelPlayerStatus = @"TruthDuelPlayerStatus";
        public const string TruthDuelRule = @"TruthDuelRule";
        public const string TruthDuelSelect = @"TruthDuelSelect";
        public const string TruthDungeon = @"TruthDungeon";
        public const string TruthEquipmentShop = @"TruthEquipmentShop";
        public const string TruthHomeTown = @"TruthHomeTown";
        public const string TruthInformation = @"TruthInformation";
        public const string TruthItemBank = @"TruthItemBank";
        public const string TruthItemDesc = @"TruthItemDesc";
        public const string TruthPlayback = @"TruthPlayback";
        public const string TruthAchievement = @"TruthAchievement";
        public const string TruthPlayerInformation = @"TruthPlayerInformation";
        public const string TruthPotionShop = @"TruthPotionShop";
        public const string TruthRequestFood = @"TruthRequestFood";
        public const string TruthMonsterQuest = @"TruthMonsterQuest";
        public const string TruthSelectCharacter = @"TruthSelectCharacter";
        public const string TruthSelectEquipment = @"TruthSelectEquipment";
        public const string TruthSkillSpellDesc = @"TruthSkillSpellDesc";
        public const string TruthStatusPlayer = @"TruthStatusPlayer";
        public const string TruthWill = @"TruthWill";
        public const string TruthRequestInput = @"TruthInputRequest";
        public const string SaveLoad = @"SaveLoad";
        public const string SingleHomeTown = @"SingleHomeTown";
        public const string SingleDungeon = @"SingleDungeon";
        public const string SingleBattleEnemy = @"SingleBattleEnemy";

        // コマンドタイミングのアイコン(add unity)
        public const string SorceryIcon = @"sorcery_mark";
        public const string NormalIcon = @"normal_mark";
        public const string InstantIcon = @"instant_mark";

        // 定形メッセージ(add unity)
        public const string exitMessage1 = @"セーブしていない場合、現在データは破棄されます。セーブしますか？";
        public const string exitMessage2 = @"タイトルへ戻りますか？";
        public const string exitMessage3 = @"アンシェットの町に戻りますか？";
        public const string exitMessage4 = @"チュートリアルを終了しますか？";
        public const string Request_Inn = @"宿屋に泊まりますか？";
        public const string Message_DuelAvailable = @"【DUEL闘技場へ行く事が出来るようになりました】";
        public const string Message_BattleSettingAvailable = @"【ESCメニューより「バトル設定」が選択できるようになりました】";
        public const string Message_PotionShopAvailable = @"『ラナのランラン薬品店♪』という看板がアインの目に入った。";
        public const string Message_GoToAnotherField = @"アインは別の場所へと飛ばされてしまった";
        public const string Message_GoToAnotherField_Back = @"アインはダンジョンゲートの裏広場に戻ってきた";
        public const string Message_GateAvailable = @"【ゲート裏の転送装置へ行けるようになりました】";
        public const string Message_MonsterQuestAvailable = @"【 モンスター討伐が行えるようになりました 】";
        public const string Message_SaveRequest1 = @"タイトルへ戻ります。今までのデータをセーブしますか？";
        public const string Message_SaveRequest2 = @"セーブしない場合、現在までのデータが破棄されます。セーブしますか？";
        public const string Message_GotoDownstair = @"アイン：下り階段発見！さっそく降りるとするか？";
        public const string Message_GotoUpstair = @"アイン：上の階へ戻る階段だな。ここは一旦戻るか？";
        public const string Message_GotoSkipMirror = @"ラナ：台座ルートまで通じる鏡にワープできるわ。使ってみる？";
        public const string Message_OriginOrNormal = @"ラナ：あれ？　原点解が分かったんだから５つ鏡の方に行ってみるんじゃないの？";
        public const string Message_Floor4Area3Lever = @"「事実」のレバーを倒しますか？";
        public const string Message_Floor4Area3Lever2 = @"「真実」のレバーを倒しますか？";

        // 原点解
        public static List<int> OriginNumber = new List<int> {1, 3, 4, 5, 4, 2, 3, 1, 4, 2, 5, 5};

        // コマンド影響因子向けアイコン
        public const string WeaponIcon = @"WeaponMark";
        public const string StrengthIcon = @"StrengthMark";
        public const string AgilityhIcon = @"AgilityMark";
        public const string IntelligenceIcon = @"IntelligenceMark";
        public const string StaminaIcon = @"StaminaMark";
        public const string MindIcon = @"MindMark";

        // Duel称号
        public const string TITLE_HONOR_1 = @"無名の新参者";
        public const string TITLE_HONOR_2 = @"オル・ランディスの弟子";
        public const string TITLE_HONOR_3 = @"ベテラン・キラー";
        public const string TITLE_HONOR_4 = @"DUELマスター";
        public const string TITLE_HONOR_5 = @"TOP Duelist 8";
        public const string TITLE_HONOR_6 = @"伝説を継ぐもの";
        public const string TITLE_HONOR_7 = @"DUEL闘技場の覇者";

        // SQLログ
        public const string LOG_GAME_START = @"GameStart";
        public const string LOG_START_SEEKER = @"StartSeeker";
        public const string LOG_LOAD_GAME = @"LoadGame";
        public const string LOG_CONFIG = @"Config";
        public const string LOG_PLAYER_STATUS = @"PlayerStatus";
        public const string LOG_BATTLE_SETTING = @"BattleSetting";
        public const string LOG_SAVE_GAME = @"SaveGame";
        public const string LOG_EXIT_GAME = @"ExitGame";
        public const string LOG_FROM_TITLE = @"FromTitle";
        public const string LOG_DUNGEON_GO = @"DungeonGo";
        public const string LOG_DUEL_ENTRANCE = @"DuelEntrance";
        public const string LOG_POTION_SHOP = @"PotionShop";
        public const string LOG_EQUIP_SHOP = @"EquipShop";
        public const string LOG_TRANSPORT_GATE = @"TransportGate";
        public const string LOG_CALL_KAHLHANZ = @"CallKahlhanz";
        public const string LOG_CALL_FAZILCASTLE = @"CallFazilCastle";
        public const string LOG_TALK_LANA = @"TalkLana";
        public const string LOG_INN = @"Inn";
        public const string LOG_CALL_RESTINN = @"CallRestInn";
        public const string LOG_CALL_ITEMBANK = @"CallItemBank";
        public const string LOG_DESCRIPTION = @"Description";
        public const string LOG_VIEW_DUNGEON = @"Viewdungeon";
        public const string LOG_PLAYBACK = @"Playback";
        public const string LOG_ACHIEVEMENT = @"Achievement";
        public const string LOG_BACKTO_TOWN = @"BackToTown";
        public const string LOG_STATUS_BASIC = @"StatusBasic";
        public const string LOG_STATUS_BACKPACK = @"StatusBackpack";
        public const string LOG_STATUS_SPELL = @"StatusSpell";
        public const string LOG_STATUS_RESIST = @"StatusResist";
        public const string LOG_STATUS_CLOSE = @"StatusClose";
        public const string LOG_STATUS_SELECTPLAYER = @"StatusSelectPlayer";
        public const string LOG_STATUS_MAINWEAPON = @"StatusMainWeapon";
        public const string LOG_STATUS_SUBWEAPON = @"StatusSubWeapon";
        public const string LOG_STATUS_ARMOR = @"StatusArmor";
        public const string LOG_STATUS_ACCESSORY1 = @"StatusAccessory1";
        public const string LOG_STATUS_ACCESSORY2 = @"StatusAccessory2";
        public const string LOG_SELECTEQUIP_NUMBER = @"SelectEquipNumber";
        public const string LOG_SELECTEQUIP_EQUIP = @"SelectEquipEquip";
        public const string LOG_SELECTEQUIP_CANCEL = @"SelectEquipCancel";
        public const string LOG_SELECTEQUIP_DROP = @"SelectEquipDrop";
        public const string LOG_REQUESTFOOD_NUMBER = @"RequestFoodNumber";
        public const string LOG_REQUESTFOOD_FOOD = @"RequestFoodFood";
        public const string LOG_REQUESTFOOD_ORDER = @"RequestFoodOrder";
        public const string LOG_SAVELOAD_PAGE = @"SaveLoadPage";
        public const string LOG_SAVELOAD_NUMBER = @"SaveLoadNumber";
        public const string LOG_SAVELOAD_CLOSE = @"SaveLoadClose";
        public const string LOG_BATTLEENEMY_BATTLE_START = @"BattleEnemyBattleStart";
        public const string LOG_BATTLEENEMY_BATTLESETTING = @"BattleEnemyBattleSetting";
        public const string LOG_BATTLEENEMY_USEITEM = @"BattleEnemyUseItem";
        public const string LOG_BATTLEENEMY_BATTLE_LOG = @"BattleLog";
        public const string LOG_BATTLEENEMY_RUNAWAY = @"BattleEnemyRunAway";
        public const string LOG_BATTLEENEMY_PLAYER = @"BattleEnemyPlayer";
        public const string LOG_BATTLEENEMY_ENEMY = @"BattleEnemyEnemy";
        public const string LOG_BATTLEENEMY_COMMAND1 = @"BattleEnemyCommand1";
        public const string LOG_BATTLEENEMY_COMMAND2 = @"BattleEnemyCommand2";
        public const string LOG_BATTLEENEMY_COMMAND3 = @"BattleEnemyCommand3";
        public const string LOG_BATTLESET_CHOICESTART = @"BattleSettingChoiceStart";
        public const string LOG_BATTLESET_CHOICEEND = @"BattleSettingChoiceEnd";
        public const string LOG_BATTLESET_PLAYERFIRST = @"BattleSettingPlayerFirst";
        public const string LOG_BATTLESET_PLAYERSECOND = @"BattleSettingPlayerSecond";
        public const string LOG_BATTLESET_PLAYERTHIRD = @"BattleSettingPlayerThird";
        public const string LOG_BATTLESET_CLOSE = @"BattleSettingClose";
        public const string LOG_ITEMBANK_LEFTPAGE = @"ItemBankLeftPage";
        public const string LOG_ITEMBANK_LEFTITEM = @"ItemBankLeftItem";
        public const string LOG_ITEMBANK_RIGHTPAGE = @"ItemBankRightPage";
        public const string LOG_ITEMBANK_RIGHTITEM = @"ItemBankRightItem";
        public const string LOG_ITEMBANK_RIGHTTOLEFT = @"ItemBankRightToLeft";
        public const string LOG_ITEMBANK_LEFTTORIGHT = @"ItemBankLeftToRight";
        public const string LOG_ITEMBANK_PLAYER = @"ItemBankPlayer";
        public const string LOG_ITEMBANK_CLOSE = @"ItemBankClose";
        public const string LOG_EQUIPSHOP_LEVEL = @"EquipShopLevel";
        public const string LOG_EQUIPSHOP_PLAYER1 = @"EquipShopPlayer1";
        public const string LOG_EQUIPSHOP_PLAYER2 = @"EquipShopPlayer2";
        public const string LOG_EQUIPSHOP_PLAYER3 = @"EquipShopPlayer3";
        public const string LOG_EQUIPSHOP_VENDORITEM = @"EquipShopVendorItem";
        public const string LOG_EQUIPSHOP_PLAYERITEM = @"EquipShopPlayerItem";
        public const string LOG_EQUIPSHOP_PLAYEREQUIPITEM = @"EquipShopPlayerEquipItem";
        public const string LOG_EQUIPSHOP_YES = @"EquipShopYes";
        public const string LOG_EQUIPSHOP_NO = @"EquipShopNo";
        public const string LOG_EQUIPSHOP_EXCHANGE = @"EquipShopExchange";
        public const string LOG_EQUIPSHOP_CLOSE = @"EquipShopClose";
        public const string LOG_PLAYBACK_CLOSE = @"PlaybackClose";
        // overrideクラスによる二重出力は不要。
        //public const string LOG_POTIONSHOP_LEVEL = @"PotionShopLevel";
        //public const string LOG_POTIONSHOP_PLAYER1 = @"PotionShopPlayer1";
        //public const string LOG_POTIONSHOP_PLAYER2 = @"PotionShopPlayer2";
        //public const string LOG_POTIONSHOP_PLAYER3 = @"PotionShopPlayer3";
        //public const string LOG_POTIONSHOP_VENDORITEM = @"PotionShopVendorItem";
        //public const string LOG_POTIONSHOP_PLAYERITEM = @"PotionShopPlayerItem";
        //public const string LOG_POTIONSHOP_PLAYEREQUIPITEM = @"PotionShopPlayerEquipItem";
        //public const string LOG_POTIONSHOP_YES = @"PotionShopYes";
        //public const string LOG_POTIONSHOP_NO = @"PotionShopNo";
        //public const string LOG_POTIONSHOP_EXCHANGE = @"PotionShopExchange";
        //public const string LOG_POTIONSHOP_CLOSE = @"PotionShopClose";

        public const string ARCHIVEMENT_FIRST_TRY = @"first_try";
        public const string ARCHIVEMENT_FIRST_DUNGEON = @"first_dungeon";
        public const string ARCHIVEMENT_COMPLETE_FLOOR1 = @"complete_floor1";
        public const string ARCHIVEMENT_COMPLETE_FLOOR2 = @"complete_floor2";
        public const string ARCHIVEMENT_COMPLETE_FLOOR3 = @"complete_floor3";
        public const string ARCHIVEMENT_COMPLETE_FLOOR4 = @"complete_floor4";
        public const string ARCHIVEMENT_COMPLETE_REBOOT = @"complete_reboot"; // 最下層から始まりへ
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION1 = @"truth_recollection1";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION2 = @"truth_recollection2";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION31 = @"truth_recollection31";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION32 = @"truth_recollection32";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION33 = @"truth_recollection33";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION34 = @"truth_recollection34";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION4 = @"truth_recollection4";
        public const string ARCHIVEMENT_TRUTH_RECOLLECTION5 = @"truth_recollection5";
        public const string ARCHIVEMENT_TRUTH_BATTLE_START_LANA = @"truthbattle_start_lana";
        public const string ARCHIVEMENT_TRUTH_BATTLE_END_LANA = @"truthbattle_end_lana";
        public const string ARCHIVEMENT_TRUTH_BATTLE_START_KAHLHANZ = @"truthbattle_start_kahlhanz";
        public const string ARCHIVEMENT_TRUTH_BATTLE_END_KAHLHANZ = @"truthbattle_end_kahlhanz";
        public const string ARCHIVEMENT_TRUTH_BATTLE_START_OL = @"truthbattle_start_ol";
        public const string ARCHIVEMENT_TRUTH_BATTLE_END_OL = @"truthbattle_end_ol";
        public const string ARCHIVEMENT_TRUTH_BATTLE_START_VERZE_1 = @"truthbattle_start_verze1";
        public const string ARCHIVEMENT_TRUTH_BATTLE_END_VERZE_1 = @"truthbattle_end_verze1";
        public const string ARCHIVEMENT_TRUTH_BATTLE_START_VERZE_2 = @"truthbattle_start_verze2";
        public const string ARCHIVEMENT_TRUTH_BATTLE_END_VERZE_2 = @"truthbattle_end_verze2";
        public const string ARCHIVEMENT_EPILOGUE = @"epilogue";
        public const string ARCHIVEMENT_ENDING = @"ending";
        public const string ARCHIVEMENT_DUEL_WIN_X = @"duel_win_";
        public const string ARCHIVEMENT_DUEL_LOSE_X = @"duel_lose_";

        // GUI text
        #region "タイトル"
        public const string GUI_TITLE_TUTORIAL = "チュートリアル";
        public const string GUI_TITLE_GAMESTART = "ゲームスタート";
        public const string GUI_TITLE_SEEKER = "シーカーモード";
        public const string GUI_TITLE_LOAD = "ロード";
        public const string GUI_TITLE_CONFIG = "コンフィグ";
        public const string GUI_TITLE_EXIT = "終了";
        #endregion
        #region "メインメニュー"
        public const string GUI_MENU_STATUS = "ステータス";
        public const string GUI_MENU_BATTLESETTING = "バトル設定";
        public const string GUI_MENU_SAVE = "セーブ";
        public const string GUI_MENU_LOAD = "ロード";
        public const string GUI_MENU_EXIT = "ゲーム終了";
        #endregion
        #region "ステータス画面/DUEL対戦相手"
        public const string GUI_STATUS_STATUS = "ステータス";
        public const string GUI_STATUS_BACKPACK = "バックパック";
        public const string GUI_STATUS_SPELL = "スペル";
        public const string GUI_STATUS_RESIST = "レジスト";

        public const string GUI_S_BASIC_NAME = "名前";
        public const string GUI_S_BASIC_LEVEL = "レベル";
        public const string GUI_S_BASIC_EXP = "経験値";
        public const string GUI_S_BASIC_GOLD = "所持金";

        public const string GUI_S_BASIC_CORE = "コア";
        public const string GUI_S_BASIC_BASIC = "基本";
        public const string GUI_S_BASIC_EQUIP = "装備";
        public const string GUI_S_BASIC_FOOD = "食事";
        public const string GUI_S_BASIC_TOTAL = "総合";

        public const string GUI_S_BASIC_LIFE = "ライフ";
        public const string GUI_S_BASIC_MANA = "マナ";
        public const string GUI_S_BASIC_SKILL = "スキル";

        public const string GUI_S_BASIC_STR = "力";
        public const string GUI_S_BASIC_AGI = "技";
        public const string GUI_S_BASIC_INT = "知";
        public const string GUI_S_BASIC_STA = "体";
        public const string GUI_S_BASIC_MIN = "心";

        public const string GUI_S_BASIC_MAIN = "メイン";
        public const string GUI_S_BASIC_SUB = "サブ";
        public const string GUI_S_BASIC_ARMOR = "防具";
        public const string GUI_S_BASIC_ACCESSORY1 = "アクセサリ１";
        public const string GUI_S_BASIC_ACCESSORY2 = "アクセサリ２";

        public const string GUI_S_BASIC_PATK = "物理攻撃";
        public const string GUI_S_BASIC_PDEF = "物理防御";
        public const string GUI_S_BASIC_MATK = "魔法攻撃";
        public const string GUI_S_BASIC_MDEF = "魔法防御";
        public const string GUI_S_BASIC_BSPD = "戦闘速度";
        public const string GUI_S_BASIC_BRCT = "戦闘反応";
        public const string GUI_S_BASIC_PTCL = "潜在能力";

        public const string GUI_S_BASIC_CLOSE = "閉じる";

        public const string GUI_S_BACKPACK_USE = "使う";
        public const string GUI_S_BACKPACK_HANDOVER = "渡す";
        public const string GUI_S_BACKPACK_DISBAND = "捨てる";
        #endregion
        #region "装備変更"
        public const string GUI_S_EQUIP_DROP = "装備を外す";
        public const string GUI_S_EQUIP_CANCEL = "キャンセル";
        #endregion
        #region "セーブ・ロード"
        public const string GUI_SAVELOAD_SAVE = "セーブ";
        public const string GUI_SAVELOAD_LOAD = "ロード";
        #endregion
        #region "ガンツの武具店/ラナの薬品店"
        public const string GUI_EQUIPSHOP_TITLE = "天下一品　ガンツの武具店";
        public const string GUI_EQUIPSHOP_TITLE_POTION = "ラナのランラン薬品店♪";
        public const string GUI_EQUIPSHOP_ITEM = "販売アイテム";
        public const string GUI_EQUIPSHOP_COST = "値段";
        public const string GUI_EQUIPSHOP_BACKPACK = "バックパック";
        public const string GUI_EQUIPSHOP_STACK = "個数";
        public const string GUI_EQUIPSHOP_TOSELL = ">>\r\n\r\n売却\r\n画面\r\n\r\n>>";
        public const string GUI_EQUIPSHOP_TOSELL_EN = ">>\r\n\r\nSELL\r\nITEM\r\n\r\n>>";
        public const string GUI_EQUIPSHOP_TOBUY = "<<\r\n\r\n購入\r\n画面\r\n\r\n<<";
        public const string GUI_EQUIPSHOP_TOBUY_EN = "<<\r\n\r\nBUY\r\nITEM\r\n\r\n<<";
        public const string GUI_EQUIPSHOP_BUY_BUTTON = "購入";
        public const string GUI_EQUIPSHOP_SELL_BUTTON = "売却";
        public const string GUI_EQUIPSHOP_CLOSE = "店を出る";
        #endregion
        #region "ハンナの倉庫番"
        public const string GUI_HANNA_TITLE = "ハンナの倉庫番";
        public const string GUI_HANNA_ITEM = "倉庫アイテム";
        public const string GUI_HANNA_BACKPACK = "バックパック";
        public const string GUI_HANNA_CLOSE = "倉庫から出る";
        #endregion
        #region "ハンナの定食屋"
        public const string GUI_HANNA_INN = "家庭料理　ハンナの定食屋";
        public const string GUI_HANNA_ORDEROK = "注文決定";
        #endregion
        #region "キャラクター選択"
        public const string GUI_SELECT_C_RESET = "やり直し";
        public const string GUI_SELECT_C_CHOICE = "選定";
        public const string GUI_SELECT_C_FIX = "確定";
        #endregion
        #region "オブジェクティブ"
        public const string GUI_OBJECTIVE = "目標";
        #endregion
        #region "バトル設定"
        public const string GUI_B_SETTING_BASIC = "基本";
        public const string GUI_B_SETTING_LIGHT = "聖";
        public const string GUI_B_SETTING_SHADOW = "闇";
        public const string GUI_B_SETTING_FIRE = "火";
        public const string GUI_B_SETTING_ICE = "水";
        public const string GUI_B_SETTING_FORCE = "理";
        public const string GUI_B_SETTING_WILL = "空";
        public const string GUI_B_SETTING_ACTIVE = "動";
        public const string GUI_B_SETTING_PASSIVE = "静";
        public const string GUI_B_SETTING_SOFT = "柔";
        public const string GUI_B_SETTING_HARD = "剛";
        public const string GUI_B_SETTING_TRUTH = "心眼";
        public const string GUI_B_SETTING_VOID = "無心";
        public const string GUI_B_SETTING_MIXSPELL = "複合\r\n魔法";
        public const string GUI_B_SETTING_MIXSKILL = "複合\r\nスキル";
        public const string GUI_B_SETTING_BASIC_COMMAND = "基本コマンド";
        public const string GUI_B_SETTING_ADVANCED_COMMAND = "魔法／スキル";
        public const string GUI_B_SETTING_COMPLETE = "完了";
        #endregion
        #region "戦闘"
        public const string GUI_BATTLE_TURN = "ターン";
        public const string GUI_BATTLE_SPEED = "時間速度";
        public const string GUI_BATTLE_GO = "戦闘開始！";
        public const string GUI_BATTLE_RUN = "戦闘中・・・";
        public const string GUI_BATTLE_RUNDUEL = "DUEL中・・・";
        public const string GUI_BATTLE_STOP = "戦闘停止";
        #endregion

        #region "ダンジョンエリア情報"
        #region "Tutorial"
        public static int[] Area0_00 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_01 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_02 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_03 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_04 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_05 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_06 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_07 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_08 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_09 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_10 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_11 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_12 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_13 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_14 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_15 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_16 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_17 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_18 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_19 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_20 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_21 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_22 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_23 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_24 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_25 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_26 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_27 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_28 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_29 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_30 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_31 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_32 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_33 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_34 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_35 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_36 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_37 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_38 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area0_39 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        #endregion
        #region "1F"
        public static int[] Area1_00 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_01 = { 5, 4, 4, 4, 4, 4, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_02 = { 5, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_03 = { 5, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 5, 5, 5, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 2, 2, 2, 2, 2, 5, 5, 2, 5 };
        public static int[] Area1_04 = { 5, 4, 4, 4, 4, 4, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 2, 5, 2, 5, 5, 2, 5 };
        public static int[] Area1_05 = { 5, 4, 4, 4, 4, 4, 5, 5, 4, 5, 5, 4, 4, 4, 4, 4, 4, 5, 5, 5, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 2, 5, 2, 2, 2, 2, 5 };
        public static int[] Area1_06 = { 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 4, 4, 4, 4, 4, 4, 4, 5, 5, 4, 4, 4, 4, 4, 4, 4, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1, 5, 1, 1, 1, 1, 1, 5, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_07 = { 5, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 5, 2, 5, 5, 5, 5, 5, 5, 5, 2, 5, 1, 5, 1, 1, 1, 1, 1, 5, 2, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_08 = { 5, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 2, 5, 1, 1, 1, 1, 5, 5, 2, 5, 1, 5, 1, 1, 1, 1, 1, 5, 2, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_09 = { 5, 4, 4, 4, 4, 4, 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 1, 5, 5, 5, 5, 5, 2, 5, 1, 5, 1, 1, 1, 1, 1, 5, 2, 2, 5, 2, 2, 2, 2, 2, 2, 5, 2, 2, 2, 5 };
        public static int[] Area1_10 = { 5, 4, 4, 4, 4, 4, 5, 5, 5, 5, 3, 5, 5, 5, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 5, 1, 5, 5, 5, 5, 5, 2, 5, 1, 5, 1, 1, 1, 1, 1, 5, 5, 2, 5, 5, 5, 5, 5, 5, 2, 5, 2, 5, 5, 5 };
        public static int[] Area1_11 = { 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 3, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 2, 5, 1, 5, 1, 1, 1, 1, 1, 5, 1, 5, 1, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 5, 5 };
        public static int[] Area1_12 = { 5, 4, 4, 4, 4, 4, 5, 5, 4, 5, 3, 5, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 2, 5, 1, 5, 1, 5, 5, 5, 1, 5, 1, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 2, 5, 5 };
        public static int[] Area1_13 = { 5, 5, 4, 5, 4, 5, 5, 5, 4, 5, 3, 3, 3, 3, 3, 3, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_14 = { 5, 5, 4, 5, 4, 4, 4, 5, 4, 5, 5, 5, 5, 5, 5, 3, 3, 5, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 1, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_15 = { 5, 5, 4, 5, 5, 5, 4, 5, 4, 4, 4, 4, 5, 3, 5, 5, 3, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_16 = { 5, 5, 4, 4, 5, 5, 4, 5, 4, 5, 5, 4, 4, 3, 3, 3, 3, 5, 2, 5, 5, 2, 2, 2, 5, 5, 1, 1, 1, 5, 1, 5, 1, 5, 1, 1, 1, 5, 1, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_17 = { 5, 5, 5, 4, 5, 5, 3, 5, 5, 5, 5, 5, 5, 3, 5, 5, 5, 5, 2, 5, 5, 2, 5, 2, 5, 5, 1, 5, 5, 5, 1, 5, 1, 5, 5, 5, 5, 5, 1, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_18 = { 5, 3, 5, 4, 5, 5, 3, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 2, 2, 5, 5, 2, 5, 2, 5, 5, 1, 5, 5, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_19 = { 5, 3, 5, 4, 5, 5, 3, 5, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 5, 2, 5, 2, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_20 = { 5, 3, 5, 4, 5, 5, 3, 5, 5, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5, 2, 2, 2, 5, 2, 5, 5, 1, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_21 = { 5, 3, 5, 4, 5, 3, 3, 5, 5, 5, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 2, 2, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_22 = { 5, 3, 5, 4, 5, 3, 5, 5, 3, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 5, 2, 5, 5 };
        public static int[] Area1_23 = { 5, 3, 5, 3, 5, 3, 5, 5, 3, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1, 5, 5, 5, 5, 5, 5, 2, 5, 5 };
        public static int[] Area1_24 = { 5, 3, 5, 3, 5, 3, 5, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 5, 1, 5, 2, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_25 = { 5, 3, 5, 3, 5, 3, 5, 3, 5, 5, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 2, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 5, 1, 5, 5, 5, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_26 = { 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 3, 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 2, 5, 5, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 5, 1, 5, 2, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_27 = { 5, 3, 5, 3, 5, 3, 5, 3, 3, 3, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 1, 5, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 2, 5, 5, 5, 5, 1, 5, 2, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_28 = { 5, 3, 5, 3, 5, 3, 5, 5, 5, 5, 5, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 5, 1, 5, 1, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 2, 5, 5, 2, 5, 2, 5, 2, 2, 5, 1, 5, 2, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_29 = { 5, 3, 5, 3, 5, 3, 3, 3, 3, 3, 3, 3, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1, 5, 1, 5, 5, 1, 5, 5, 1, 1, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 5, 2, 2, 2, 1, 5, 2, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_30 = { 5, 3, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1, 5, 1, 5, 5, 5, 5, 1, 1, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 2, 2, 5, 1, 5, 5, 5, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_31 = { 5, 3, 5, 3, 5, 3, 3, 3, 3, 3, 3, 3, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1, 1, 1, 5, 5, 5, 1, 1, 5, 5, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_32 = { 5, 3, 5, 3, 5, 3, 5, 5, 5, 5, 5, 3, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5, 1, 1, 5, 5, 2, 2, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 5 };
        public static int[] Area1_33 = { 5, 3, 5, 3, 5, 3, 3, 3, 3, 3, 5, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 5, 5, 2, 2, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 2, 5 };
        public static int[] Area1_34 = { 5, 3, 5, 3, 5, 5, 5, 5, 5, 3, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 5 };
        public static int[] Area1_35 = { 5, 3, 5, 3, 5, 5, 5, 5, 5, 3, 5, 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 2, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_36 = { 5, 3, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 3, 3, 3, 3, 3, 3, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 2, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_37 = { 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 2, 5 };
        public static int[] Area1_38 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5 };
        public static int[] Area1_39 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

        #endregion
        #region "2F"
        public static int[] Area2_00 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area2_01 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1 };
        public static int[] Area2_02 = { 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 4, 4, 4, 5, 1 };
        public static int[] Area2_03 = { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 4, 5, 1 };
        public static int[] Area2_04 = { 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5, 5, 1, 5, 1, 5, 2, 2, 2, 5, 2, 2, 2, 5, 2, 2, 2, 5, 1, 5, 4, 4, 4, 4, 4, 5, 4, 5, 1 };
        public static int[] Area2_05 = { 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 1, 5, 1, 5, 2, 2, 2, 5, 2, 2, 2, 5, 2, 2, 2, 5, 1, 5, 4, 4, 4, 4, 4, 4, 4, 5, 1 };
        public static int[] Area2_06 = { 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 1, 5, 1, 5, 2, 2, 2, 5, 2, 2, 2, 5, 2, 2, 2, 5, 1, 5, 4, 4, 4, 4, 4, 5, 5, 5, 1 };
        public static int[] Area2_07 = { 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 1, 5, 1, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1 };
        public static int[] Area2_08 = { 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area2_09 = { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 5, 5, 5, 5, 1, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1 };
        public static int[] Area2_10 = { 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 5, 5, 5, 5, 1, 5, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 2, 2, 2, 5, 1 };
        public static int[] Area2_11 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 1, 5, 3, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 1, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1 };
        public static int[] Area2_12 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 1, 5, 3, 5, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 5, 1, 5, 5, 5, 5, 5, 2, 2, 2, 5, 1 };
        public static int[] Area2_13 = { 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 3, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 1, 5, 2, 2, 2, 5, 5, 5, 5, 5, 1 };
        public static int[] Area2_14 = { 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 3, 5, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 5, 1, 5, 2, 2, 2, 2, 2, 2, 2, 2, 1 };
        public static int[] Area2_15 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 1, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 1, 5, 2, 2, 2, 5, 5, 5, 5, 5, 1 };
        public static int[] Area2_16 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 5, 5, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 5, 1, 5, 5, 5, 5, 5, 2, 2, 2, 5, 1 };
        public static int[] Area2_17 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 1, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5 };
        public static int[] Area2_18 = { 5, 5, 2, 2, 2, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 5, 1, 1, 1, 5, 5, 5, 3, 5, 3, 5, 3, 5, 3, 5, 3, 5, 5, 1, 5, 5, 5, 5, 5, 2, 2, 2, 5, 5 };
        public static int[] Area2_19 = { 5, 5, 2, 5, 2, 5, 5, 2, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area2_20 = { 5, 5, 2, 5, 2, 2, 2, 2, 5, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 2, 5, 1, 1, 1, 5, 5, 1, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5 };
        public static int[] Area2_21 = { 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 2, 2, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area2_22 = { 5, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 2, 5, 5, 5, 1, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area2_23 = { 5, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 1, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area2_24 = { 5, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 5, 1, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1 };
        public static int[] Area2_25 = { 5, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 5, 4, 4, 4, 5, 5, 5, 2, 2, 2, 2, 2, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1 };
        public static int[] Area2_26 = { 5, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 1, 5, 2, 2, 2, 2, 2, 5, 1, 5, 5, 2, 2, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1 };
        public static int[] Area2_27 = { 5, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 5, 4, 4, 4, 5, 5, 5, 2, 2, 2, 2, 2, 5, 1, 5, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area2_28 = { 5, 5, 5, 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 5, 4, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 1, 5, 5, 2, 2, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1 };
        public static int[] Area2_29 = { 5, 5, 5, 3, 3, 3, 3, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 1, 5, 5, 1, 5, 5, 2, 2, 2, 5, 5, 5, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1 };
        public static int[] Area2_30 = { 5, 5, 5, 3, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 1, 5, 5, 1, 5, 5, 2, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area2_31 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 1, 5, 5, 1, 5, 5, 2, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area2_32 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 1, 1, 5, 5, 1, 5, 5, 2, 2, 2, 5, 5, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 3, 3 };
        public static int[] Area2_33 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 1, 5, 5, 5, 1, 5, 5, 2, 2, 2, 2, 2, 3, 3, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        public static int[] Area2_34 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 1, 5, 5, 5, 1, 5, 5, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        public static int[] Area2_35 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 1, 1, 1, 1, 5, 1, 5, 5, 2, 2, 2, 2, 2, 3, 3, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 5, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        public static int[] Area2_36 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 1, 1, 1, 1, 5, 1, 5, 5, 5, 5, 5, 5, 5, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 3, 3 };
        public static int[] Area2_37 = { 5, 3, 3, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5, 4, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 1, 5, 5, 5, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4 };
        public static int[] Area2_38 = { 5, 5, 5, 5, 5, 4, 5, 5, 5, 5, 5, 5, 5, 5, 4, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 1, 5, 5, 5, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4 };
        public static int[] Area2_39 = { 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        #endregion
        #region "3F"
        public static int[] Area3_00 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_01 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_02 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_03 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_04 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_05 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_06 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_07 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_08 = { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_09 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_10 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_11 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_12 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_13 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_14 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_15 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_16 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_17 = { 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_18 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_19 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_20 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_21 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_22 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_23 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_24 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_25 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_26 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_27 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_28 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_29 = { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_30 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_31 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_32 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_33 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_34 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_35 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_36 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_37 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_38 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
        public static int[] Area3_39 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        #endregion
        #region "4F"
        public static int[] Area4_00 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_01 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_02 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_03 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_04 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_05 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_06 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_07 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_08 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_09 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_10 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_11 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_12 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_13 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_14 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_15 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_16 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_17 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area4_18 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 6, 6, 6, 6, 6, 6 };
        public static int[] Area4_19 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
        public static int[] Area4_20 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 1, 1, 1, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 6, 6, 6 };
        public static int[] Area4_21 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_22 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_23 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_24 = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_25 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_26 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 6, 6, 6, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_27 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 6, 6, 6, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_28 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_29 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_30 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_31 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_32 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 5, 5, 5, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_33 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_34 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_35 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_36 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_37 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_38 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        public static int[] Area4_39 = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        #endregion
        #region "5F"
        public static int[] Area5_00 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area5_01 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area5_02 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_03 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_04 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_05 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_06 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_07 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_08 = { 5, 5, 1, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_09 = { 5, 5, 1, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_10 = { 5, 5, 1, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_11 = { 5, 5, 1, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_12 = { 5, 5, 1, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_13 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_14 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_15 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_16 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_17 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_18 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_19 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_20 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_21 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_22 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_23 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_24 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_25 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_26 = { 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_27 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_28 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5 };
        public static int[] Area5_29 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_30 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_31 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_32 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_33 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_34 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_35 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int[] Area5_36 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area5_37 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area5_38 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area5_39 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        #endregion
        #region "エスミリア草原区域１"
        public static int[] Area1_1_00 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_01 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_02 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_03 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_04 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_05 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_06 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_07 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_08 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_09 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_10 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_11 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_12 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_13 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_14 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_15 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_16 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 5, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_17 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 5, 5, 5, 1, 1, 1, 1, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_18 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 5, 5, 5, 1, 1, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_19 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 5, 5, 5, 5, 1, 5, 1, 5, 5, 1, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_20 = { 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 1, 5, 1, 5, 5, 1, 1, 1, 1, 5, 5, 1, 1, 1, 5, 5, 1, 1, 1, 1, 5, 5, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_21 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 5, 5, 1, 5, 5, 1, 1, 1, 1, 1, 1, 5, 5, 1, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_22 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 5, 5, 1, 5, 5, 1, 1, 1, 5, 1, 1, 5, 1, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_23 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 1, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_24 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_25 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_26 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_27 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_28 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_29 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_30 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_31 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_32 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_33 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_34 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_35 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_36 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_37 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_38 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_1_39 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        #endregion
        #region "エスミリア草原区域２"
        public static int[] Area1_2_00 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_01 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_02 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_03 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_04 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_05 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_06 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_07 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_08 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_09 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_10 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_11 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_12 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_13 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_14 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_15 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 1, 5, 5, 5, 5, 1, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_16 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 1, 1, 1, 1, 1, 5, 1, 5, 5, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 5, 5, 1, 1, 5, 5, 1, 5, 5, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_17 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 5, 1, 1, 1, 5, 1, 1, 1, 5, 5, 5, 1, 5, 5, 5, 5, 1, 5, 5, 5, 5, 1, 1, 5, 5, 5, 1, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_18 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 1, 1, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_19 = { 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 1, 1, 1, 1, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_20 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 1, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 1, 5, 1, 5, 5, 1, 1, 5, 1, 1, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_21 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 5, 5, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 1, 5, 5, 5, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_22 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 5, 1, 5, 5, 5, 1, 5, 5, 5, 5, 5, 5, 1, 5, 1, 1, 1, 5, 5, 5, 1, 5, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_23 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 5, 5, 1, 5, 5, 5, 1, 5, 1, 5, 1, 1, 1, 1, 1, 1, 5, 1, 1, 5, 5, 1, 5, 5, 5, 1, 5, 5, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_24 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 1, 5, 1, 5, 1, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_25 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_26 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_27 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_28 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_29 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_30 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_31 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_32 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_33 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_34 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_35 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_36 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_37 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_38 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        public static int[] Area1_2_39 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        #endregion
        #endregion
        #region "ダンジョンタイル情報"
        #region "Tutorial"
        public static string[] Tile0_00 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR" };
        public static string[] Tile0_01 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_02 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_03 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_04 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_05 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_06 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_07 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_08 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_09 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_10 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_11 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_12 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_13 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_14 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_15 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_16 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_17 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_18 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_19 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Normal-1EdgeTL", "Tile1-Normal", "Normal-1EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Normal-1EdgeTL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_20 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Normal-1EdgeLB", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_21 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Normal-1EdgeLB", "Tile1-Normal", "Normal-1EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Normal-1EdgeLB", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_22 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_23 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_24 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_25 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_26 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_27 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Normal-1EdgeLB", "Normal-1EdgeTR", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_28 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Normal-1EdgeLB", "Normal-1EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_29 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Normal-1EdgeLB", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_30 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_31 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_32 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_33 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_34 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_35 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_36 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_37 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_38 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile0_39 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        #endregion
        #region "1F"
        public static string[] Tile1_00 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR" };
        public static string[] Tile1_01 = { "Tile1-WallL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallR" };
        public static string[] Tile1_02 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_03 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_04 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_05 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallR" };
        public static string[] Tile1_06 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_07 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallR" };
        public static string[] Tile1_08 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_09 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallR" };
        public static string[] Tile1_10 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_11 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_12 = { "Tile1-WallL", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_13 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_14 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_15 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_16 = { "Tile1-WallL", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_17 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_18 = { "Tile1-WallL", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_19 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_20 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_21 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_22 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_23 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_24 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallR" };
        public static string[] Tile1_25 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_26 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallR" };
        public static string[] Tile1_27 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallR" };
        public static string[] Tile1_28 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallR" };
        public static string[] Tile1_29 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallR" };
        public static string[] Tile1_30 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_31 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_32 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_33 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_34 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_35 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_36 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_37 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR" };
        public static string[] Tile1_38 = { "Tile1-WallL", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallR" };
        public static string[] Tile1_39 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        #endregion
        #region "2F"
        public static string[] Tile2_00 = { "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile2_01 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_02 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_03 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_04 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_05 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_06 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_07 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_08 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR" };
        public static string[] Tile2_09 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_10 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_11 = { "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_12 = { "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_13 = { "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_14 = { "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR" };
        public static string[] Tile2_15 = { "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_16 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLRB" };
        public static string[] Tile2_17 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_18 = { "Tile1-WallL", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_19 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_20 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_21 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_22 = { "Tile1-WallL", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_23 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile2_24 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_25 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL" };
        public static string[] Tile2_26 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-WallL", "Normal-1EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-WallL", "Normal-1EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal-WallR" };
        public static string[] Tile2_27 = { "Tile1-WallL", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal-WallR" };
        public static string[] Tile2_28 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Normal-1EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-WallL", "Normal-1EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-WallL", "Normal-1EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal-WallR" };
        public static string[] Tile2_29 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB" };
        public static string[] Tile2_30 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_31 = { "Tile1-WallL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile2_32 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile2_33 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Normal-1EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Normal-1EdgeTL", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Normal-1EdgeTL", "Tile1-Normal-WallR", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile2_34 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile2_35 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Normal-1EdgeLB", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Normal-1EdgeLB", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile2_36 = { "Tile1-WallL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile2_37 = { "Tile1-WallL", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_38 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR" };
        public static string[] Tile2_39 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB" };
        #endregion
        #region "3F"
        public static string[] Tile3_00 = { "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_01 = { "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_02 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_03 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_04 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_05 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_06 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_07 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Normal-1EdgeTL", "Normal-1EdgeTR", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_08 = { "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Normal-3Edge_LB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_09 = { "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_10 = { "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_11 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_12 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_13 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLRB", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_14 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Normal-1EdgeTL", "Normal-1EdgeTR", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_15 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_16 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_17 = { "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_18 = { "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Normal-3Edge_LB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_19 = { "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_20 = { "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Normal-1EdgeTR", "Tile1-Normal-WallT", "Normal-3Edge_LB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_21 = { "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_22 = { "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_23 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_24 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_25 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Normal-3Edge_TL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_26 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_27 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_28 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_29 = { "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_30 = { "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_31 = { "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-Normal-NearTL", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_32 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_33 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal-NearR", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_34 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile3_35 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile3_36 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallT", "Tile1-WallB", "Tile1-WallT", "Tile1-WallB", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-Normal-NearL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_37 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLRB", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_38 = { "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile3_39 = { "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        #endregion
        #region "4F"
        public static string[] Tile4_00 = { "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile4_01 = { "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR" };
        public static string[] Tile4_02 = { "Tile1-Normal-NearTL", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallTLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR" };
        public static string[] Tile4_03 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Normal-3Edge_TL", "Tile1-WallRB" };
        public static string[] Tile4_04 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR" };
        public static string[] Tile4_05 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR" };
        public static string[] Tile4_06 = { "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR" };
        public static string[] Tile4_07 = { "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR" };
        public static string[] Tile4_08 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallT", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR" };
        public static string[] Tile4_09 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR" };
        public static string[] Tile4_10 = { "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearT-EdgeLB", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB" };
        public static string[] Tile4_11 = { "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-NearR", "Tile1-WallR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile4_12 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeRB", "Tile1-Normal-NearRB", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR" };
        public static string[] Tile4_13 = { "Tile1-Normal-WallL", "Tile1-Normal-WallR", "Tile1-WallLB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR" };
        public static string[] Tile4_14 = { "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR" };
        public static string[] Tile4_15 = { "Tile1-WallTLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallRB", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearR-EdgeLB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB" };
        public static string[] Tile4_16 = { "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile4_17 = { "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT-EdgeRB", "Tile1-WallR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB" };
        public static string[] Tile4_18 = { "Tile1-WallL", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR" };
        public static string[] Tile4_19 = { "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR" };
        public static string[] Tile4_20 = { "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB" };
        public static string[] Tile4_21 = { "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallB", "Tile1-Normal-NearB-EdgeTL", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearR-EdgeLB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile4_22 = { "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR" };
        public static string[] Tile4_23 = { "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallLR" };
        public static string[] Tile4_24 = { "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR" };
        public static string[] Tile4_25 = { "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB" };
        public static string[] Tile4_26 = { "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearL-EdgeTR", "Tile1-Normal-NearTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile4_27 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR" };
        public static string[] Tile4_28 = { "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR" };
        public static string[] Tile4_29 = { "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearT", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallLR" };
        public static string[] Tile4_30 = { "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR" };
        public static string[] Tile4_31 = { "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-Normal-WallL", "Tile1-Normal-NearR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR" };
        public static string[] Tile4_32 = { "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearT", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-WallT", "Tile1-Normal-NearR-EdgeTL", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB" };
        public static string[] Tile4_33 = { "Tile1-WallLRB", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-NearTL", "Tile1-Normal-NearTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR" };
        public static string[] Tile4_34 = { "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallRB", "Tile1-Normal-NearLB", "Tile1-Normal-NearB-EdgeTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR" };
        public static string[] Tile4_35 = { "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-Normal-NearTL", "Tile1-Normal-WallT", "Tile1-Normal-NearTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR" };
        public static string[] Tile4_36 = { "Tile1-WallTL", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-Normal-WallL", "Tile1-Normal", "Tile1-Normal-WallR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallLR" };
        public static string[] Tile4_37 = { "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-Normal-NearL", "Tile1-Normal", "Tile1-Normal-NearR", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR" };
        public static string[] Tile4_38 = { "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-Normal-NearLB", "Tile1-Normal-WallB", "Tile1-Normal-NearRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR" };
        public static string[] Tile4_39 = { "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB" };
        #endregion
        #region "5F"
        public static string[] Tile5_00 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTLR", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR" };
        public static string[] Tile5_01 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_02 = { "Tile1-WallL", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_03 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_04 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_05 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_06 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_07 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_08 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_09 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_10 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_11 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_12 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_13 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_14 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_15 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_16 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_17 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_18 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_19 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_20 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_21 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_22 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_23 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_24 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_25 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_26 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_27 = { "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_28 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_29 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallTR" };
        public static string[] Tile5_30 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_31 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_32 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_33 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_34 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_35 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        public static string[] Tile5_36 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_37 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_38 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile5_39 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        #endregion
        #region "エスミリア草原区域１"
        public static string[] Tile1_1_00 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR" };
        public static string[] Tile1_1_01 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_02 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_03 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_04 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_05 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_06 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_07 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_08 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_09 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_10 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_11 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_12 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_13 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_14 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_15 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_16 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_17 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTR", "Tile1", "Tile1-WallTLB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_18 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_19 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_20 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_21 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_22 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_23 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_24 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_25 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_26 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_27 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_28 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_29 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_30 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_31 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_32 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_33 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_34 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_35 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_36 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_37 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_38 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_1_39 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        #endregion
        #region "エスミリア草原区域２"
        public static string[] Tile1_2_00 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR" };
        public static string[] Tile1_2_01 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_02 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_03 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_04 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_05 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_06 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_07 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_08 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_09 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_10 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_11 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_12 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_13 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_14 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_15 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_16 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallT", "Tile1-WallTRB", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_17 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_18 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_19 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_20 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_21 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_22 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_23 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_24 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_25 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_26 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_27 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_28 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_29 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_30 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_31 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_32 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_33 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_34 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_35 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_36 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_37 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_38 = { "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        public static string[] Tile1_2_39 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        #endregion
        //public static string[] Tile1_1 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLB", "Tile1", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        //public static string[] Tile1_2 = { "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallTB", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        //public static string[] Tile2_1 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        //public static string[] Tile2_2 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTRB", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB-DummyB", "Tile1-WallL", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB" };
        //public static string[] Tile3_1 = { "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        //public static string[] Tile3_2 = { "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallR", "Tile1-WallTLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallTLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallT", "Tile1-WallB", "Tile1-WallT", "Tile1-WallB", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLRB", "Tile1-WallTLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLRB", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        //public static string[] Tile4_1 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR" };
        //public static string[] Tile4_2 = { "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallTLB", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTLR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLRB", "Tile1-WallTL", "Tile1", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallB", "Tile1", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTLR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTRB", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallT", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLRB", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallL", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLR", "Tile1-WallLB", "Tile1", "Tile1", "Tile1", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallT", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallTR", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallLB", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTL", "Tile1-WallTR", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLR", "Tile1-WallLR", "Tile1-WallLRB", "Tile1-WallLR", "Tile1-WallLB", "Tile1-WallTRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallTLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallRB", "Tile1-WallLB", "Tile1-WallTB", "Tile1-WallRB" };
        //public static string[] Tile5_1 = { "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTLR", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR" };
        //public static string[] Tile5_2 = { "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallTL", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallTR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLR", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1-WallT", "Tile1-WallTR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTL", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1-WallT", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1-WallTB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallL", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1", "Tile1-WallR", "Tile1-WallLB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallB", "Tile1-WallRB" };
        #endregion
        #region "コンテンツ"
        public static string[] Content0 = { "TreasureInfo810", "BoardInfo1158", "UpstairInfo1230", "BoardInfo1241", "TreasureInfo1954" };
        public static string[] Content1 = { "TreasureInfo82", "TreasureInfo100", "TreasureInfo113", "BlueWallRightInfo125", "BlueWallLeftInfo126", "BoardInfo217", "BoardInfo363", "BlueWallRightInfo365", "BlueWallLeftInfo366", "DownstairInfo377", "TreasureInfo499", "TreasureInfo513", "BoardInfo522", "TreasureInfo529", "TreasureInfo547", "BlueWallRightInfo615", "BlueWallLeftInfo616", "BlueWallRightInfo665", "BlueWallLeftInfo666", "BlueWallBottomInfo722", "BlueWallBottomInfo724", "TreasureInfo744", "BlueWallTopInfo782", "BlueWallTopInfo784", "TreasureInfo814", "UpstairInfo879", "TreasureInfo968", "BlueWallRightInfo972", "BlueWallLeftInfo973", "TreasureInfo1081", "TreasureInfo1089", "TreasureInfo1245", "TreasureInfo1313", "BoardInfo1314", "TreasureInfo1328", "TreasureInfo1492", "BlueWallBottomInfo1574", "BlueWallBottomInfo1581", "TreasureInfo1612", "BlueWallTopInfo1634", "BlueWallTopInfo1641", "BlueWallRightInfo1692", "BlueWallLeftInfo1693", "BlueWallRightInfo1702", "BlueWallLeftInfo1703", "TreasureInfo1723", "TreasureInfo1769", "BoardInfo1878", "BlueWallRightInfo1992", "BlueWallLeftInfo1993", "BlueWallRightInfo2002", "BlueWallLeftInfo2003", "BlueWallBottomInfo2054", "BlueWallBottomInfo2061", "BlueWallTopInfo2114", "BlueWallTopInfo2121", "TreasureInfo2126", "TreasureInfo2134", "TreasureInfo2172", "TreasureInfo2328" };
        public static string[] Content2 = { "BlueWallBottomInfo162", "BlueWallRightInfo169", "BlueWallLeftInfo170", "BlueWallTopInfo222", "BoardInfo351", "TreasureInfo355", "TreasureInfo389", "BoardInfo390", "BlueWallBottomInfo458", "BlueWallBottomInfo466", "BlueWallBottomInfo515", "BlueWallTopInfo518", "BlueWallTopInfo526", "BoardInfo529", "BlueWallTopInfo575", "BlueWallRightInfo709", "BlueWallLeftInfo710", "TreasureInfo755", "BlueWallRightInfo898", "BlueWallLeftInfo899", "TreasureInfo913", "BlueWallRightInfo927", "BlueWallLeftInfo928", "BoardInfo929", "BoardInfo942", "BlueWallRightInfo946", "BlueWallLeftInfo947", "TreasureInfo1019", "BlueWallBottomInfo1053", "BlueWallRightInfo1069", "BlueWallLeftInfo1070", "BlueWallRightInfo1090", "BlueWallLeftInfo1091", "BlueWallTopInfo1113", "TreasureInfo1144", "TreasureInfo1162", "BoardInfo1165", "UpstairInfo1169", "BoardInfo1173", "BlueWallBottomInfo1225", "BlueWallTopInfo1285", "BlueWallBottomInfo1401", "BoardInfo1409", "BlueWallRightInfo1410", "BlueWallLeftInfo1411", "BlueWallTopInfo1461", "TreasureInfo1559", "BoardInfo1574", "BlueWallRightInfo1576", "DownstairInfo1577", "BlueWallLeftInfo1577", "BlueWallBottomInfo1626", "BoardInfo1653", "TreasureInfo1654", "BoardInfo1665", "TreasureInfo1666", "BoardInfo1677", "TreasureInfo1686", "BlueWallTopInfo1686", "BlueWallBottomInfo1694", "BlueWallTopInfo1754", "TreasureInfo1882", "BlueWallBottomInfo2061", "TreasureInfo2074", "BoardInfo2075", "TreasureInfo2086", "BoardInfo2087", "TreasureInfo2098", "BoardInfo2099", "BlueWallTopInfo2121", "BlueWallBottomInfo2225", "BoardInfo2250", "BlueWallTopInfo2285", "TreasureInfo2350", "TreasureInfo2363", "TreasureInfo2371" };
        public static string[] Content3 = { "TreasureInfo3", "MirrorInfo11", "MirrorInfo13", "MirrorInfo39", "MirrorInfo40", "MirrorInfo41", "DownstairInfo59", "MirrorInfo64", "MirrorInfo66", "MirrorInfo77", "MirrorInfo81", "MirrorInfo87", "MirrorInfo113", "BoardInfo115", "MirrorInfo145", "MirrorInfo151", "MirrorInfo159", "MirrorInfo175", "MirrorInfo176", "MirrorInfo177", "MirrorInfo178", "MirrorInfo179", "MirrorInfo195", "MirrorInfo201", "MirrorInfo204", "MirrorInfo205", "MirrorInfo207", "MirrorInfo209", "MirrorInfo214", "MirrorInfo222", "MirrorInfo244", "TreasureInfo259", "MirrorInfo261", "MirrorInfo270", "MirrorInfo275", "MirrorInfo280", "MirrorInfo284", "MirrorInfo285", "MirrorInfo292", "MirrorInfo315", "MirrorInfo321", "MirrorInfo336", "MirrorInfo338", "MirrorInfo346", "MirrorInfo350", "MirrorInfo355", "MirrorInfo356", "MirrorInfo357", "MirrorInfo358", "MirrorInfo359", "MirrorInfo370", "MirrorInfo382", "MirrorInfo406", "MirrorInfo411", "MirrorInfo412", "MirrorInfo424", "TreasureInfo440", "MirrorInfo445", "MirrorInfo447", "MirrorInfo449", "MirrorInfo453", "BoardInfo457", "MirrorInfo481", "MirrorInfo489", "MirrorInfo497", "MirrorInfo502", "MirrorInfo510", "MirrorInfo521", "MirrorInfo535", "MirrorInfo536", "MirrorInfo537", "MirrorInfo538", "MirrorInfo539", "TreasureInfo541", "MirrorInfo551", "MirrorInfo561", "MirrorInfo575", "MirrorInfo585", "MirrorInfo587", "MirrorInfo590", "MirrorInfo591", "MirrorInfo593", "MirrorInfo604", "MirrorInfo617", "BoardInfo627", "MirrorInfo630", "MirrorInfo642", "MirrorInfo652", "MirrorInfo654", "MirrorInfo681", "MirrorInfo684", "MirrorInfo692", "MirrorInfo693", "MirrorInfo694", "MirrorInfo699", "MirrorInfo701", "MirrorInfo705", "MirrorInfo714", "MirrorInfo715", "MirrorInfo716", "MirrorInfo717", "MirrorInfo718", "MirrorInfo719", "MirrorInfo741", "MirrorInfo750", "MirrorInfo757", "MirrorInfo761", "MirrorInfo772", "MirrorInfo784", "MirrorInfo810", "MirrorInfo812", "MirrorInfo819", "MirrorInfo820", "MirrorInfo824", "MirrorInfo832", "MirrorInfo853", "MirrorInfo862", "MirrorInfo864", "MirrorInfo866", "MirrorInfo885", "MirrorInfo887", "MirrorInfo895", "MirrorInfo896", "MirrorInfo897", "MirrorInfo898", "MirrorInfo899", "MirrorInfo929", "MirrorInfo942", "MirrorInfo951", "MirrorInfo964", "MirrorInfo971", "BoardInfo984", "MirrorInfo990", "MirrorInfo992", "MirrorInfo994", "MirrorInfo1000", "BoardInfo1003", "MirrorInfo1009", "MirrorInfo1013", "MirrorInfo1021", "MirrorInfo1026", "MirrorInfo1035", "MirrorInfo1041", "MirrorInfo1043", "MirrorInfo1046", "MirrorInfo1055", "MirrorInfo1062", "MirrorInfo1063", "TreasureInfo1065", "MirrorInfo1073", "MirrorInfo1075", "MirrorInfo1076", "MirrorInfo1077", "MirrorInfo1078", "MirrorInfo1079", "MirrorInfo1080", "MirrorInfo1099", "MirrorInfo1103", "MirrorInfo1107", "MirrorInfo1121", "MirrorInfo1124", "MirrorInfo1128", "UpstairInfo1140", "BoardInfo1149", "MirrorInfo1157", "BlueWallRightInfo1158", "BlueWallLeftInfo1159", "MirrorInfo1168", "MirrorInfo1176", "BoardInfo1177", "BoardInfo1209", "MirrorInfo1218", "MirrorInfo1219", "MirrorInfo1220", "MirrorInfo1229", "MirrorInfo1231", "MirrorInfo1245", "MirrorInfo1246", "MirrorInfo1248", "MirrorInfo1250", "MirrorInfo1252", "MirrorInfo1255", "MirrorInfo1256", "MirrorInfo1257", "MirrorInfo1258", "MirrorInfo1259", "MirrorInfo1262", "MirrorInfo1264", "MirrorInfo1284", "MirrorInfo1295", "MirrorInfo1302", "BoardInfo1307", "MirrorInfo1313", "MirrorInfo1321", "MirrorInfo1329", "MirrorInfo1341", "MirrorInfo1345", "MirrorInfo1346", "MirrorInfo1351", "MirrorInfo1368", "MirrorInfo1370", "MirrorInfo1372", "MirrorInfo1373", "MirrorInfo1384", "MirrorInfo1404", "MirrorInfo1406", "MirrorInfo1408", "MirrorInfo1409", "MirrorInfo1414", "MirrorInfo1419", "MirrorInfo1435", "MirrorInfo1436", "MirrorInfo1437", "MirrorInfo1438", "MirrorInfo1439", "TreasureInfo1442", "MirrorInfo1446", "TreasureInfo1459", "MirrorInfo1461", "MirrorInfo1471", "MirrorInfo1482", "MirrorInfo1484", "MirrorInfo1493", "MirrorInfo1513", "MirrorInfo1515", "MirrorInfo1522", "MirrorInfo1533", "MirrorInfo1534", "MirrorInfo1564", "MirrorInfo1566", "MirrorInfo1570", "MirrorInfo1582", "MirrorInfo1584", "MirrorInfo1594", "MirrorInfo1597", "MirrorInfo1600", "MirrorInfo1604", "MirrorInfo1615", "MirrorInfo1616", "MirrorInfo1617", "MirrorInfo1618", "MirrorInfo1619", "MirrorInfo1632", "MirrorInfo1641", "MirrorInfo1660", "TreasureInfo1668", "MirrorInfo1673", "MirrorInfo1674", "MirrorInfo1703", "MirrorInfo1707", "MirrorInfo1709", "MirrorInfo1711", "MirrorInfo1714", "MirrorInfo1720", "MirrorInfo1723", "MirrorInfo1727", "MirrorInfo1730", "MirrorInfo1732", "MirrorInfo1734", "MirrorInfo1744", "MirrorInfo1761", "MirrorInfo1765", "MirrorInfo1766", "MirrorInfo1776", "MirrorInfo1780", "MirrorInfo1783", "MirrorInfo1795", "MirrorInfo1796", "MirrorInfo1797", "MirrorInfo1798", "MirrorInfo1799", "MirrorInfo1818", "MirrorInfo1821", "MirrorInfo1827", "MirrorInfo1829", "MirrorInfo1831", "MirrorInfo1834", "MirrorInfo1838", "MirrorInfo1841", "MirrorInfo1861", "MirrorInfo1866", "MirrorInfo1870", "MirrorInfo1884", "MirrorInfo1897", "MirrorInfo1907", "MirrorInfo1908", "MirrorInfo1924", "MirrorInfo1941", "MirrorInfo1954", "MirrorInfo1974", "MirrorInfo1975", "MirrorInfo1976", "MirrorInfo1977", "MirrorInfo1978", "MirrorInfo1979", "MirrorInfo1986", "MirrorInfo1997", "MirrorInfo2001", "MirrorInfo2010", "MirrorInfo2017", "MirrorInfo2019", "TreasureInfo2027", "MirrorInfo2029", "MirrorInfo2031", "MirrorInfo2067", "MirrorInfo2071", "MirrorInfo2073", "TreasureInfo2080", "MirrorInfo2081", "MirrorInfo2083", "MirrorInfo2085", "MirrorInfo2089", "MirrorInfo2104", "MirrorInfo2117", "MirrorInfo2121", "MirrorInfo2123", "MirrorInfo2125", "MirrorInfo2127", "MirrorInfo2137", "MirrorInfo2142", "MirrorInfo2155", "MirrorInfo2156", "MirrorInfo2157", "MirrorInfo2158", "MirrorInfo2159", "MirrorInfo2166", "MirrorInfo2189", "MirrorInfo2196", "MirrorInfo2203", "MirrorInfo2209", "MirrorInfo2213", "BoardInfo2240", "MirrorInfo2242", "MirrorInfo2244", "MirrorInfo2246", "MirrorInfo2258", "MirrorInfo2265", "MirrorInfo2273", "MirrorInfo2284", "MirrorInfo2289", "MirrorInfo2291", "MirrorInfo2301", "TreasureInfo2311", "MirrorInfo2318", "MirrorInfo2320", "MirrorInfo2329", "MirrorInfo2333", "MirrorInfo2334", "TreasureInfo2343", "MirrorInfo2357", "MirrorInfo2375", "MirrorInfo2382", "MirrorInfo2387", "BlueWallRightInfo2394", "BlueWallLeftInfo2395", "BoardInfo2396", "DownstairInfo2399" };
        public static string[] Content4 = { "TreasureInfo0", "TreasureInfo5", "TreasureInfo12", "TreasureInfo18", "TreasureInfo27", "TreasureInfo44", "TreasureInfo111", "BlueWallRightInfo115", "BlueWallLeftInfo116", "BlueWallRightInfo131", "BlueWallLeftInfo132", "TreasureInfo143", "TreasureInfo147", "BoardInfo150", "BoardInfo177", "TreasureInfo190", "BlueWallBottomInfo211", "BoardInfo217", "TreasureInfo226", "BlueWallRightInfo233", "BlueWallLeftInfo234", "TreasureInfo258", "BlueWallTopInfo271", "TreasureInfo272", "BlueWallRightInfo275", "BlueWallLeftInfo276", "BoardInfo292", "TreasureInfo296", "BlueWallBottomInfo345", "TreasureInfo361", "TreasureInfo368", "TreasureInfo378", "TreasureInfo384", "BlueWallTopInfo405", "BlueWallRightInfo407", "BlueWallLeftInfo408", "TreasureInfo420", "BlueWallBottomInfo426", "BlueWallBottomInfo430", "BlueWallRightInfo431", "BlueWallBottomInfo431", "BlueWallLeftInfo432", "TreasureInfo433", "TreasureInfo448", "TreasureInfo449", "BoardInfo466", "BlueWallTopInfo486", "BlueWallTopInfo490", "BlueWallRightInfo490", "BoardInfo491", "BlueWallTopInfo491", "BlueWallLeftInfo491", "BlueWallRightInfo491", "BlueWallLeftInfo492", "TreasureInfo517", "TreasureInfo543", "TreasureInfo553", "TreasureInfo555", "TreasureInfo558", "TreasureInfo592", "TreasureInfo610", "TreasureInfo617", "TreasureInfo621", "TreasureInfo630", "BlueWallBottomInfo643", "BoardInfo650", "TreasureInfo659", "TreasureInfo661", "TreasureInfo687", "BoardInfo695", "BlueWallTopInfo703", "TreasureInfo707", "BlueWallRightInfo711", "BlueWallLeftInfo712", "TreasureInfo725", "TreasureInfo740", "TreasureInfo749", "BlueWallRightInfo753", "BlueWallLeftInfo754", "TreasureInfo762", "BoardInfo764", "TreasureInfo788", "BlueWallBottomInfo796", "TreasureInfo802", "TreasureInfo805", "TreasureInfo820", "BlueWallRightInfo820", "BlueWallLeftInfo821", "BlueWallTopInfo856", "TreasureInfo857", "TreasureInfo860", "TreasureInfo862", "TreasureInfo901", "TreasureInfo907", "TreasureInfo923", "TreasureInfo927", "TreasureInfo943", "BlueWallBottomInfo980", "BlueWallBottomInfo1005", "TreasureInfo1006", "BlueWallBottomInfo1007", "TreasureInfo1014", "BlueWallTopInfo1040", "TreasureInfo1043", "TreasureInfo1047", "BlueWallTopInfo1065", "BlueWallTopInfo1067", "TreasureInfo1088", "BlueWallRightInfo1128", "BlueWallLeftInfo1129", "UpstairInfo1132", "BlueWallRightInfo1157", "BlueWallLeftInfo1158", "BoardInfo1160", "BoardInfo1186", "MirrorInfo1189", "TreasureInfo1204", "TreasureInfo1214", "MirrorInfo1232", "MirrorInfo1240", "BlueWallRightInfo1248", "BlueWallLeftInfo1249", "DownstairInfo1252", "TreasureInfo1273", "MirrorInfo1295", "TreasureInfo1310", "MirrorInfo1342", "MirrorInfo1346", "MirrorInfo1348", "MirrorInfo1352", "MirrorInfo1364", "BlueWallRightInfo1368", "BlueWallLeftInfo1369", "TreasureInfo1379", "TreasureInfo1383", "TreasureInfo1387", "TreasureInfo1393", "TreasureInfo1398", "MirrorInfo1411", "MirrorInfo1423", "TreasureInfo1442", "TreasureInfo1450", "TreasureInfo1467", "MirrorInfo1475", "TreasureInfo1495", "TreasureInfo1498", "MirrorInfo1522", "MirrorInfo1526", "TreasureInfo1527", "MirrorInfo1534", "BoardInfo1576", "MirrorInfo1592", "MirrorInfo1594", "MirrorInfo1603", "BoardInfo1629", "MirrorInfo1643", "TreasureInfo1651", "TreasureInfo1675", "BoardInfo1691", "MirrorInfo1703", "BoardInfo1707", "UpstairInfo1711", "MirrorInfo1712", "TreasureInfo1718", "TreasureInfo1724", "TreasureInfo1739", "TreasureInfo1747", "BoardInfo1754", "MirrorInfo1761", "MirrorInfo1763", "MirrorInfo1771", "MirrorInfo1835", "MirrorInfo1839", "TreasureInfo1843", "TreasureInfo1856", "BoardInfo1862", "BoardInfo1866", "BoardInfo1868", "TreasureInfo1886", "MirrorInfo1887", "MirrorInfo1888", "MirrorInfo1904", "BlueWallBottomInfo1908", "TreasureInfo1910", "TreasureInfo1916", "TreasureInfo1918", "BoardInfo1926", "MirrorInfo1943", "TreasureInfo1947", "MirrorInfo1951", "MirrorInfo1959", "BlueWallTopInfo1968", "TreasureInfo1975", "BoardInfo1980", "BoardInfo1985", "BoardInfo1988", "BoardInfo1990", "BoardInfo1991", "TreasureInfo1994", "BlueWallRightInfo1997", "BlueWallLeftInfo1998", "MirrorInfo2020", "TreasureInfo2033", "TreasureInfo2045", "BoardInfo2048", "BoardInfo2054", "BoardInfo2057", "BoardInfo2060", "BoardInfo2086", "BlueWallRightInfo2088", "BlueWallLeftInfo2089", "BlueWallRightInfo2090", "BlueWallLeftInfo2091", "BoardInfo2092", "TreasureInfo2098", "TreasureInfo2110", "BoardInfo2111", "BlueWallRightInfo2122", "BlueWallLeftInfo2123", "TreasureInfo2140", "BlueWallRightInfo2150", "BlueWallLeftInfo2151", "TreasureInfo2163", "TreasureInfo2172", "BoardInfo2174", "TreasureInfo2189", "TreasureInfo2193", "TreasureInfo2197", "BoardInfo2224", "BoardInfo2238", "TreasureInfo2251", "TreasureInfo2255", "TreasureInfo2259", "BoardInfo2262", "BoardInfo2341", "BoardInfo2347", "TreasureInfo2350", "TreasureInfo2354", "BlueWallRightInfo2362", "BlueWallLeftInfo2363", "TreasureInfo2379", "TreasureInfo2381", "BlueWallRightInfo2387", "BlueWallLeftInfo2388" };
        public static string[] Content5 = { "BlueOrbInfo122", "UpstairInfo177", "FountainInfo871", "UpstairInfo1710" };

        public static string[] Content1_1 = { "TreasureInfo927", "TreasureInfo1040", "TreasureInfo1061", "TreasureInfo1116", "BlueOrbInfo1179", "UpstairInfo1205", "TreasureInfo1215", "FountainInfo1242", "EnemyInfo1244", "DownstairInfo1248", "FountainInfo1285", "TreasureInfo1359" };
        public static string[] Content1_2 = { "TreasureInfo973", "FountainInfo975", "BlueOrbInfo978", "TreasureInfo993", "TreasureInfo1042", "TreasureInfo1063", "UpstairInfo1130", "DownstairInfo1147", "EnemyInfo1151", "FountainInfo1167", "TreasureInfo1355", "TreasureInfo1362", "TreasureInfo1402" };
        #endregion

        #region "アイテム名称英語"
        public const string EN_HINJAKU_ARMRING = "Poor Ring";
        public const string EN_USUYOGORETA_FEATHER = "Dirty Feather";
        public const string EN_NON_BRIGHT_ORB = "Orb with No-Shine";
        public const string EN_KUKEI_BANGLE = "Rectangle Bangle";
        public const string EN_SUTERARESHI_EMBLEM = @"Emblem of ThrowedAway";
        public const string EN_ARIFURETA_STATUE = @"Poor Statue";
        public const string EN_NON_ADJUST_BELT = @"Unadjustable Belt";
        public const string EN_SIMPLE_EARRING = @"Poor Earring";
        public const string EN_KATAKUZURESHITA_FINGERRING = @"Collapsed Ring";
        public const string EN_IROASETA_CHOKER = @"Faded Choker";
        public const string EN_YOREYORE_MANTLE = @"Bad Form Cloak";
        public const string EN_NON_HINSEI_CROWN = @"Rudeness Crown";
        public const string EN_TUKAIFURUSARETA_SWORD = @"Tired Sword";
        public const string EN_TUKAINIKUI_LONGSWORD = @"Useless Long-Sword";
        public const string EN_GATAGAKITERU_ARMOR = @"Damaged Armor";
        public const string EN_FESTERING_ARMOR = @"Dirty Armor";
        public const string EN_HINSO_SHIELD = @"Poor Shield";
        public const string EN_MUDANIOOKII_SHIELD = @"Wasteful Shield";

        public const string EN_RED_PENDANT = @"Red Pendant";
        public const string EN_BLUE_PENDANT = @"Blue Pendant";
        public const string EN_PURPLE_PENDANT = @"Purple Pendant";
        public const string EN_GREEN_PENDANT = @"Green Pendant";
        public const string EN_YELLOW_PENDANT = @"Yellow Pendant";
        public const string EN_SISSO_ARMRING = @"Simplicity Ring";
        public const string EN_FINE_FEATHER = @"Fine Feather";
        public const string EN_KIREINA_ORB = @"Beautiful Orb";
        public const string EN_FIT_BANGLE = @"Fit Bungle";
        public const string EN_PRISM_EMBLEM = @"Prism Emblem";
        public const string EN_FINE_SWORD = @"Fine Sword";
        public const string EN_TWEI_SWORD = @"Twei-Sword";
        public const string EN_FINE_ARMOR = @"Fine Armor";
        public const string EN_GOTHIC_PLATE = @"Gothic Plate";
        public const string EN_FINE_SHIELD = @"Fine Shield";
        public const string EN_GRIPPING_SHIELD = @"Gripping Shield";

        public const string EN_JOUSITU_BLUE_POWERRING = @"Superior Blue-Power Ring";
        public const string EN_KOUJOUSINYADORU_RED_ORB = @"Ambitional-Red Orb";
        public const string EN_MAGICIANS_MANTLE = @"Magician's Mantle";
        public const string EN_BEATRUSH_BANGLE = @"Beat-Rush Bangle";
        public const string EN_AERO_BLADE = @"Aero Blade";

        public const string EN_OLD_USELESS_ROD = @"Poor Rod";
        public const string EN_KISSAKI_MARUI_TUME = @"Non-edged Claw";
        public const string EN_BATTLE_HUMUKI_BUTOUGI = @"Unsuitable Battle-Cross";
        public const string EN_SIZE_AWANAI_ROBE = @"UnAdjustable Robe";
        public const string EN_NO_CONCEPT_RING = @"Non-worth Ring";
        public const string EN_HIGHCOLOR_MANTLE = @"Bad-style Mantle";
        public const string EN_EIGHT_PENDANT = @"Octagon Pendant";
        public const string EN_GOJASU_BELT = @"Wasteful Belt";
        public const string EN_EGARA_HUMEI_EMBLEM = @"Tired Pendant";
        public const string EN_HAYATOTIRI_ORB = @"Careless Orb";

        public const string EN_COPPER_RING_TORA = @"Copper-Ring [Tiger]";
        public const string EN_COPPER_RING_IRUKA = @"Copper-Ring [Dolphin]";
        public const string EN_COPPER_RING_UMA = @"Copper-Ring [Horse]";
        public const string EN_COPPER_RING_KUMA = @"Copper-Ring [Bear]";
        public const string EN_COPPER_RING_HAYABUSA = @"Copper-Ring [Falcon]";
        public const string EN_COPPER_RING_TAKO = @"Copper-Ring [Octopus]";
        public const string EN_COPPER_RING_USAGI = @"Copper-Ring [Rabbit]";
        public const string EN_COPPER_RING_KUMO = @"Copper-Ring [Spider]";
        public const string EN_COPPER_RING_SHIKA = @"Copper-Ring [deer]";
        public const string EN_COPPER_RING_ZOU = @"Copper-Ring [Elephant]";
        public const string EN_RED_AMULET = @"Amulet [Red]";
        public const string EN_BLUE_AMULET = @"Amulet [Blue]";
        public const string EN_PURPLE_AMULET = @"Amulet [Purple]";
        public const string EN_GREEN_AMULET = @"Amulet [Green]";
        public const string EN_YELLOW_AMULET = @"Amulet [Yellow]";
        public const string EN_SHARP_CLAW = @"Sharp Claw";
        public const string EN_LIGHT_CLAW = @"Lightweight Claw";
        public const string EN_WOOD_ROD = @"Wood Rod";
        public const string EN_SHORT_SWORD = @"Short Sword";
        public const string EN_BASTARD_SWORD = @"Bastard Sword";
        public const string EN_LETHER_CLOTHING = @"Lether Cross";
        public const string EN_COTTON_ROBE = @"Cotton Robe";
        public const string EN_COPPER_ARMOR = @"Copper Armor";
        public const string EN_HEAVY_ARMOR = @"Heavy Armor";
        public const string EN_IRON_SHIELD = @"Iron Shield";

        public const string EN_SINTYUU_RING_KUROHEBI = @"Brass-Ring [Black Snake]";
        public const string EN_SINTYUU_RING_HAKUTYOU = @"Brass-Ring [White Bird]";
        public const string EN_SINTYUU_RING_AKAHYOU = @"Brass-Ring [Red Panther]";
        public const string EN_ICE_SWORD = @"Icicle Sword";
        public const string EN_RISING_KNUCKLE = @"Thunder Knucle";
        public const string EN_AUTUMN_ROD = @"Autumn Rod";
        public const string EN_SUN_BRAVE_ARMOR = @"Brave Armor of the Sun";
        public const string EN_ESMERALDA_SHIELD = @"Shield of Esmeralda";
        #endregion

        #region "モンスター討伐"
        public const int MQ_AREA_NUM = 5;
        public const int MQ_STAGE_NUM = 20;
        public const int MQ_TREASURE_NUM = 3;
        public static string[] MQ_FLOOR1_NAME = { @"ビートルの掃除",
                                                  @"マンティス撃退",
                                                  @"マンドラゴラ討伐",
                                                  @"レッドホッパー駆除",
                                                  @"アント群の殲滅",
                                                  @"敵対するポイズンマリー",
                                                  @"攻撃的な鷹の撃退",
                                                  @"ワンダーシードの駆除",
                                                  @"フランシス護衛隊の殲滅",
                                                  @"弾丸ヒューイを撃退",
                                                  @"のさばるウルフの討伐",
                                                  @"濃霧に潜む影を駆除",
                                                  @"決戦モスグリーン・ダディ",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };

        public static string[] MQ_FLOOR2_NAME = { @"トビウオの捕獲",
                                                  @"貝殻の中の真珠",
                                                  @"幻のウミウシ",
                                                  @"凶暴なシーアーチン討伐",
                                                  @"海岸線に陣取る亀",
                                                  @"横暴なビッグマウスジョー",
                                                  @"浮遊するモーグルマンタ",
                                                  @"危険な暴れザメの討伐",
                                                  @"進軍するキャシー",
                                                  @"虹色に咲くアネモネの駆除",
                                                  @"超危険な大暴れザメの討伐",
                                                  @"深海、謎の生物エイト・エイト",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static string[] MQ_FLOOR3_NAME = { @"オークの侵攻阻止",
                                                  @"荒れ狂うマンモス",
                                                  @"氷陣のグリフィン",
                                                  @"野蛮なオーガ勢力",
                                                  @"鋭い眼光の剣歯虎",
                                                  @"怒りに満ちた大熊",
                                                  @"消えたアザラシの追跡",
                                                  @"魔法雹穴銃の出現",
                                                  @"凍り輝く結晶体",
                                                  @"獰猛なウェアウルフの出現",
                                                  @"ウィンド・ブレイカーの討伐",
                                                  @"高潔：ツンドラ・ロングホーン",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static string[] MQ_FLOOR4_NAME = { @"闇に潜むハンター達",
                                                  @"落ちぶれたシーカーの討伐",
                                                  @"暗雲に潜むメフィストの影",
                                                  @"死刑執行人の亡霊",
                                                  @"黒炎ブレイドの出現",
                                                  @"悪意を放つダークエルフ",
                                                  @"異形のアークデーモン",
                                                  @"フレイム・スラッシャーの討伐",
                                                  @"滅死：デビル・チルドレン",
                                                  @"邪悪な怨念の正体",
                                                  @"黒いオーラを放つ騎士達",
                                                  @"致死の極：ドゥームブリンガー",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static string[] MQ_FLOOR1_DESC = { @"サンディ：草原区域の道はずれにビートルが発生しているとの事。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：以前はモンスターのでなかった場所に突如マンティス達が発生し始めている。アイン・ウォーレンスよ！マンティスの撃退をお願いしたい！",
                                                  @"サンディ：普段はおとなしく生息しているマンドラゴラだが、突如凶暴化したとの事。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：年に数回発生、大量発生するイナゴが今回草原区域で確認された。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：大きくなりすぎたアントが度々、農村に出没しているとの事。アイン・ウォーレンスよ！アント群の殲滅をお願いしたい！",
                                                  @"サンディ：マリーの花は本来無害の筈なのだが、毒化が進行してしまったようだ。アイン・ウォーレンスよ！ポイズン・マリーの駆除をお願いしたい！",
                                                  @"サンディ：ターゲットを誤ってしまった鷹は残念だが今は撃退する他無い。アイン・ウォーレンスよ！鷹の軍勢の殲滅をお願いしたい！",
                                                  @"サンディ：ワンダーシードはこの地で度々通行人の邪魔を行っているとの事。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：決して遭遇してはならないフランシスの護衛隊を数人見かけた声が挙がっている。アイン・ウォーレンスよ！フランシスナイトの討伐をお願いしたい！",
                                                  @"サンディ：ショットガン・ヒューイは突如森林区域に現れたとのこと。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：森を守るのが本来の役割のはずだが、森林の入り口にまで出没している声が挙がっている。アイン・ウォーレンスよ！番狼の討伐をお願いしたい！",
                                                  @"サンディ：森林の奥地にある湖に異質な靄が発生しているのはミストエレメンタルの仕業である。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：アイン・ウォーレンスよ！森林の奥地を支配しているモスグリーン・ダディと決戦し、森林区域の平和を取り戻してほしい！よろしく頼む！",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static string[] MQ_FLOOR2_DESC = { @"サンディ：ヴェルガスの海岸区域に生息するトビウオの群れが異常活性化しているとの事。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：海底神殿の入り口に生息する貝から時折真珠が入手されるとの事。アイン・ウォーレンスよ！真珠の入手をお願いしたい！",
                                                  @"サンディ：普段ダイバーが生態観測しているウミウシが突如視界から消えたとのこと。アイン・ウォーレンスよ！消えたウミウシの調査をお願いしたい！",
                                                  @"サンディ：元々危険極まりないシーアーチンが海中で活発化しているとの事。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：頑亀はおとなしい性格ではあるが、浜辺付近を陣取っている姿が目撃された。アイン・ウォーレンスよ！頑亀を追い払っていただきたい！",
                                                  @"サンディ：しばらく息を潜めていたビッグマウスジョーが出没したとの連絡が入った。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：マンタは活動時期に入ると人を襲う傾向があり、残念だが危険な生物であると言える。アイン・ウォーレンスよ！マンタの討伐をお願いしたい！",
                                                  @"サンディ：危険度の高い鮫が海中を荒らし回っている状況である！アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：愛称キャシーで知られている大蟹だが、突如凶暴化してしまったとの連絡を受けた。アイン・ウォーレンスよ！キャシーの沈静化をお願いしたい！",
                                                  @"サンディ：海底神殿近辺に生息していたアネモネが虹色に変色しつつ危害を加えているの報告が入った。アイン・ウォーレンスよ！駆除をお願いしたい！",
                                                  @"サンディ：緊急事態である！前回より更に巨大な鮫が見境なく攻撃を繰り返している報告が入った！アイン・ウォーレンスよ！大至急、討伐をお願いしたい！",
                                                  @"サンディ：海底神殿の深い階層にて未知の生物が目撃されたとの情報が入った。アイン・ウォーレンスよ！生物の探索および討伐をお願いしたい！",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static string[] MQ_FLOOR3_DESC = { @"サンディ：雪原区域で生息していないはずのオークが突如侵入してきたとの連絡が入った。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：しばらく休眠していたマンモス達が今年も活動開始し初めたとの事！アイン・ウォーレンスよ！マンモスの撃退をお願いしたい！",
                                                  @"サンディ：雪原に住まうグリフィンは非常に攻撃性が高く、近辺に被害が発生し始めている。アイン・ウォーレンスよ！グリフィンの駆除をお願いしたい！",
                                                  @"サンディ：野蛮なオーガ共がまたしても、このファージル領域に入り込んできている。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：非常にまずい事になった。雪原の街道区域に剣歯虎が居座ってしまったとの連絡が入った。アイン・ウォーレンスよ！剣歯虎を追い払っていただきたい！",
                                                  @"サンディ：普段は人を襲わない熊達だが、時折民家を襲うとの報告もある。アイン・ウォーレンスよ！大熊の討伐をお願いしたい！",
                                                  @"サンディ：いつもの生息区域に居たアザラシが雷雲に打たれたのをきっかけに凶暴化したとの報告が入った。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：大変な連絡が入った！！魔族の一員である魔法雹穴銃が突如出現したとの事！アイン・ウォーレンスよ！大至急、討伐をお願いしたい！",
                                                  @"サンディ：氷の結晶体は本来生物ではないはずなのだが、何らかの理由により多数の攻撃が目撃されている。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：氷雪区域のウェアウルフが周辺一帯を占領する形で群を形成し始めている。アイン・ウォーレンスよ！ウェアウルフの討伐をお願いしたい！",
                                                  @"サンディ：氷山近辺にて雷雲の中からウィンドブレイカーが出現したとの報告が入った！アイン・ウォーレンスよ！ウィンドブレイカーの撃退をお願いしたい！",
                                                  @"サンディ：氷山の頂上には高潔なツンドラ・ロングホーンが生息していると噂されている。アイン・ウォーレンスよ！その生物の探索をお願いしたい！",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static string[] MQ_FLOOR4_DESC = { @"サンディ：宮殿の宝物庫はではすっかりならず者達が蔓延ってしまっている。アイン・ウォーレンスよ！ハンター達の討伐をお願いしたい。",
                                                  @"サンディ：本来ファージルの護衛を司るシーカー達が、村人を苦しめているケースがある。アイン・ウォーレンスよ！落ちぶれたシーカーの討伐をお願いしたい！",
                                                  @"サンディ：暗闇の雲が宝物庫の第７層に出現したとの報告が入った。アイン・ウォーレンスよ！暗雲の正体を確認の上、駆除をお願いしたい！",
                                                  @"サンディ：先日埋葬されたはずの死刑執行人が亡霊と化して宝物庫ゲートに現れたとの事。アイン・ウォーレンスよ！亡霊の駆除をお願いしたい！",
                                                  @"サンディ：大変な事になった！あの黒炎ブレイドが何者かによって異世界から召喚されてしまったとの目撃情報が入った。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：信じられない事だが、この地を守ってきたエルフの長が突如、攻撃性を持ち始めたとの報告が入った。アイン・ウォーレンスよ！エルフの撃退をお願いしたい！",
                                                  @"サンディ：アークデーモンがこの地に出現してしまうとは・・・一般兵ではとても太刀打ち出来ない状況である。アイン・ウォーレンスよ！何卒、討伐をお願いしたい！",
                                                  @"サンディ：とんでもない事態となった！フレイム・スラッシャーが宝物庫にて突如現れ始めた！アイン・ウォーレンスよ！大至急、討伐をお願いしたい！",
                                                  @"サンディ：魔族デビル・チルドレンがファージルエリア中央に出現し、異世界空間を展開し始めたとの連絡が入った。アイン・ウォーレンスよ！討伐をお願いしたい！",
                                                  @"サンディ：宝物庫の最深部にはダミー箱しか設置されてないはずだが、怨霊が蔓延っているとの噂が上がっている。。アイン・ウォーレンスよ！怨霊の正体を確認していただきたい！",
                                                  @"サンディ：禍々しいオーラを放つ騎士達がファージル各地にて出没している状況である。アイン・ウォーレンスよ！騎士達の討伐をお願いしたい！",
                                                  @"サンディ：死を生を司るドゥームブリンガーが・・・冥界ゲートより召喚されてしまったとの報告が入った！！アイン・ウォーレンスよ！ドゥームブリンガーの討伐をお願いしたい！",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                  @"",
                                                };
        public static int[] MQ_FLOOR1_REQUIRE = { 1,
                                                  2,
                                                  3,
                                                  5,
                                                  6,
                                                  8,
                                                  10,
                                                  10,
                                                  11,
                                                  12,
                                                  13,
                                                  13,
                                                  15,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                };
        public static int[] MQ_FLOOR2_REQUIRE = { 20,
                                                  20,
                                                  22,
                                                  23,
                                                  23,
                                                  25,
                                                  26,
                                                  26,
                                                  28,
                                                  30,
                                                  30,
                                                  32,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                };
        public static int[] MQ_FLOOR3_REQUIRE = { 35,
                                                  35,
                                                  37,
                                                  38,
                                                  38,
                                                  40,
                                                  41,
                                                  41,
                                                  43,
                                                  44,
                                                  44,
                                                  46,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                };
        public static int[] MQ_FLOOR4_REQUIRE = { 50,
                                                  50,
                                                  51,
                                                  52,
                                                  52,
                                                  53,
                                                  54,
                                                  54,
                                                  55,
                                                  56,
                                                  56,
                                                  58,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                  0,
                                                };

        public static string[] MQ_FLOOR1_ICON = { "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                  "Field_Forest",
                                                };
        public static string[] MQ_FLOOR2_ICON = { "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                  "Field_Sea",
                                                };
        public static string[] MQ_FLOOR3_ICON = { "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                  "Field_Frozen",
                                                };
        public static string[] MQ_FLOOR4_ICON = { "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                  "Field_Castle",
                                                };
        public static string[,] MQ_FLOOR1_REWARD = {
            { Database.COMMON_BEATLE_TOGATTA_TUNO, Database.COMMON_HENSYOKU_KUKI, Database.COMMON_FINE_FEATHER },
            { Database.COMMON_MANTIS_TAIEKI, Database.COMMON_GREEN_SIKISO, Database.COMMON_FINE_SHIELD },
            { Database.COMMON_MANDORAGORA_ROOT, Database.COMMON_WARM_NO_KOUKAKU, Database.COMMON_GREEN_PENDANT },
            { Database.COMMON_INAGO, Database.COMMON_SUN_LEAF, Database.COMMON_PRISM_EMBLEM },
            { Database.COMMON_ANT_ESSENCE, Database.COMMON_YELLOW_MATERIAL, Database.COMMON_SPIDER_SILK },
            { Database.RARE_MARY_KISS, Database.COMMON_ALRAUNE_KAHUN, Database.RARE_BEATRUSH_BANGLE },
            { Database.COMMON_TAKA_FETHER, Database.COMMON_RABBIT_KEGAWA, Database.COMMON_RABBIT_MEAT },
            { Database.COMMON_PLANTNOID_SEED, Database.COMMON_ASH_EGG, Database.COMMON_WOOD_ROD },
            { Database.COMMON_TOGE_HAETA_SYOKUSYU, Database.COMMON_SNEAK_UROKO, Database.COMMON_COPPER_RING_KUMO },
            { Database.RARE_HYUI_SEED, Database.COMMON_BLUE_AMULET, Database.GROWTH_LIQUID_AGILITY },
            { Database.COMMON_OOKAMI_FANG, Database.COMMON_BRILLIANT_RINPUN, Database.COMMON_BASTARD_SWORD },
            { Database.COMMON_MIST_CRYSTAL, Database.COMMON_DRYAD_RINPUN, Database.COMMON_COTTON_ROBE },
            { Database.RARE_MOSSGREEN_EKISU, Database.COMMON_RED_HOUSI, Database.RARE_SINTYUU_RING_KUROHEBI },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            };

        public static string[,] MQ_FLOOR2_REWARD = {
            { Database.COMMON_DAGGERFISH_UROKO, Database.COMMON_SIPPUU_HIRE, Database.COMMON_SMART_CLAW },
            { Database.COMMON_WHITE_MAGATAMA, Database.COMMON_BLUE_MAGATAMA, Database.COMMON_SHIOKAZE_MANTLE },
            { Database.RARE_TRANSPARENT_POWDER, Database.COMMON_BLUEWHITE_SHARP_TOGE, Database.COMMON_KURIONE_ZOUMOTU },
            { Database.COMMON_SEA_WASI_KUTIBASI, Database.COMMON_RENEW_AKAMI, Database.COMMON_PURPLE_CHARM },
            { Database.COMMON_GANGAME_EGG, Database.COMMON_WASI_BLUE_FEATHER, Database.COMMON_BRIGHT_GESO },
            { Database.RARE_JOE_ARM, Database.RARE_JOE_LEG, Database.RARE_JOE_TONGUE },
            { Database.COMMON_SOFT_BIG_HIRE, Database.COMMON_PURE_WHITE_BIGEYE, Database.RARE_BLUE_LIGHTNING },
            { Database.COMMON_SAME_NANKOTSU, Database.COMMON_GOTUGOTU_KARA, Database.COMMON_HALF_TRANSPARENT_ROCK_ASH },
            { Database.RARE_SEKIKASSYOKU_HASAMI, Database.COMMON_BRONZE_RING_HANERU, Database.GROWTH_LIQUID2_STRENGTH },
            { Database.COMMON_NANAIRO_SYOKUSYU, Database.COMMON_KOUSITUKA_MATERIAL, Database.COMMON_DENDOU_ROD },
            { Database.COMMON_AOSAME_KENSHI, Database.COMMON_AOSAME_UROKO, Database.COMMON_PUREWHITE_KIMO },
            { Database.COMMON_EIGHTEIGHT_KUROSUMI, Database.COMMON_EIGHTEIGHT_KYUUBAN, Database.RARE_BLACK_MAGICIAN_CROSS },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            };

        public static string[,] MQ_FLOOR3_REWARD = {
            { Database.COMMON_ORC_MOMONIKU, Database.COMMON_SNOW_CAT_KEGAWA, Database.COMMON_EXCELLENT_SHIELD },
            { Database.COMMON_BIG_HIZUME, Database.COMMON_FAIRY_POWDER, Database.COMMON_EXCELLENT_KNUCKLE },
            { Database.RARE_GRIFFIN_WHITE_FEATHER, Database.COMMON_RED_STONE, Database.GROWTH_LIQUID3_MIND },
            { Database.COMMON_GOTUGOTU_KONBOU, Database.COMMON_LIZARD_UROKO, Database.COMMON_EXCELLENT_ARMOR },
            { Database.COMMON_SHARPNESS_TIGER_TOOTH, Database.COMMON_KINKIN_ICE, Database.RARE_BLACK_ICE_SWORD },
            { Database.RARE_BEAR_CLAW_KAKERA, Database.COMMON_EMBLEM_OF_PENGUIN, Database.RARE_ADERKER_FALSE_ROD },
            { Database.COMMON_WHITE_AZARASHI_MEAT, Database.COMMON_TOUMEI_SNOW_CRYSTAL, Database.COMMON_CENTAURUS_LEATHER },
            { Database.COMMON_BLUE_DANGAN_KAKERA, Database.COMMON_ARGONIAN_PURPLE_UROKO, Database.COMMON_BLUE_MASEKI },
            { Database.RARE_PURE_CRYSTAL, Database.RARE_POWER_STEEL_RING_VAPOR, Database.GROWTH_LIQUID3_INTELLIGENCE },
            { Database.COMMON_WOLF_KEGAWA, Database.COMMON_CLAW_HEART, Database.COMMON_FROZEN_HEART },
            { Database.COMMON_ESSENCE_OF_WIND, Database.RARE_BLUE_LIGHT_MOON_CLAW, Database.RARE_SYUUDOUSOU_KUROOBI },
            { Database.RARE_TUNDRA_DEER_HORN, Database.COMMON_OLD_TREE_JUSHI, Database.RARE_BLUE_REFLECT_ROBE },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            };

        public static string[,] MQ_FLOOR4_REWARD = {
            { Database.COMMON_HUNTER_SEVEN_TOOL, Database.COMMON_BEAST_KEGAWA, Database.COMMON_GREEN_MEDALLION },
            { Database.COMMON_SABI_BUGU, Database.RARE_BLOOD_DAGGER_KAKERA, Database.COMMON_BIRD_SONG_LUTE },
            { Database.RARE_MEPHISTO_BLACKLIGHT, Database.RARE_DETACHMENT_ORB, Database.GROWTH_LIQUID4_STRENGTH },
            { Database.COMMON_EXECUTIONER_ROBE, Database.COMMON_NEMESIS_ESSENCE, Database.COMMON_ROYAL_GUARD_RING },
            { Database.RARE_MASTERBLADE_KAKERA, Database.RARE_MASTERBLADE_FIRE, Database.RARE_ESSENCE_OF_DARK },
            { Database.COMMON_GREAT_JEWELCROWN, Database.COMMON_SEEKER_HEAD, Database.RARE_INVISIBLE_STATE_ROD },
            { Database.RARE_DEMON_HORN, Database.RARE_ESSENCE_OF_SHINE, Database.RARE_VERDANT_SONIC_CLAW },
            { Database.COMMON_KUMITATE_TENBIN, Database.COMMON_KUMITATE_TENBIN_BOU, Database.COMMON_KUMITATE_TENBIN_DOU },
            { Database.RARE_BLACK_SEAL_IMPRESSION, Database.RARE_ESSENCE_OF_FLAME, Database.COMMON_WYVERN_BONE },
            { Database.COMMON_ONRYOU_HAKO, Database.RARE_ANGEL_SILK, Database.COMMON_PURPLE_FLOAT_STONE },
            { Database.RARE_DREAD_EXTRACT, Database.RARE_CHAOS_SIZUKU, Database.RARE_EMBLEM_OF_VALKYRIE },
            { Database.RARE_DOOMBRINGER_KAKERA, Database.RARE_DOOMBRINGER_TUKA,  Database.GROWTH_LIQUID4_MIND},
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            { "", "", "" },
            };

        public static int[] MQ_FLOOR1_GOLD = {   50,
                                                 80,
                                                120,
                                                180,
                                                210,
                                                240,
                                                300,
                                                380,
                                                460,
                                                600,
                                                700,
                                                800,
                                               1200,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                             };
        public static int[] MQ_FLOOR2_GOLD = { 1900,
                                               2100,
                                               2300,
                                               2700,
                                               3000,
                                               3300,
                                               4000,
                                               4700,
                                               5500,
                                               6000,
                                               6500,
                                               8000,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                             };
        public static int[] MQ_FLOOR3_GOLD = { 14000,
                                               15000,
                                               16000,
                                               18000,
                                               20000,
                                               22000,
                                               25000,
                                               28000,
                                               32000,
                                               36000,
                                               40000,
                                               55000,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                             };
        public static int[] MQ_FLOOR4_GOLD = {  60000,
                                                63000,
                                                66000,
                                                70000,
                                                74000,
                                                78000,
                                                82000,
                                                87000,
                                                92000,
                                               100000,
                                               120000,
                                               150000,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                               0,
                                             };

        public static int[] MQ_FLOOR1_EXP = { 100,
                                              180,
                                              240,
                                              320,
                                              380,
                                              450,
                                              560,
                                              680,
                                              800,
                                              900,
                                              1000,
                                              1100,
                                              2000,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                            };
        public static int[] MQ_FLOOR2_EXP = { 3000,
                                              3200,
                                              3400,
                                              3700,
                                              4000,
                                              4300,
                                              4700,
                                              5100,
                                              5500,
                                              6000,
                                              6500,
                                              7500,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                            };
        public static int[] MQ_FLOOR3_EXP = { 14000,
                                              15000,
                                              16000,
                                              18000,
                                              20000,
                                              22000,
                                              24000,
                                              26000,
                                              28000,
                                              30000,
                                              32000,
                                              38000,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                            };
        public static int[] MQ_FLOOR4_EXP = {  44000,
                                               48000,
                                               52000,
                                               56000,
                                               61000,
                                               66000,
                                               72000,
                                               79000,
                                               86000,
                                               94000,
                                              102000,
                                              120000,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                              0,
                                            };

        #endregion
    }
}

