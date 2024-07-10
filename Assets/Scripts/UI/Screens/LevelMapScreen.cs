using UI.Screens.Base;
using UI.Screens.Parts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public sealed class LevelMapScreen : ScreenBase
    {
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private LevelMapToggle levelMapTogglePrefab;
        [SerializeField] private RectTransform container;

        [SerializeField] private RectTransform[] levelTogglesPositions;

        [SerializeField] private Button playButton;
        
        private LevelMapToggle[] _spawnedToggles;
        private int _selectedLevel;
        
        private void Awake()
        {
            InitToggles();
        }

        private void OnEnable()
        {
            foreach (var levelMapToggle in _spawnedToggles)
            {
                levelMapToggle.onSelect += OnSelectLevelToggle;
            }
            
            playButton.onClick.AddListener(OnClickPlayButton);
        }

        private void OnDisable()
        {
            foreach (var levelMapToggle in _spawnedToggles)
            {
                levelMapToggle.onSelect -= OnSelectLevelToggle;
            }
            
            playButton.onClick.RemoveListener(OnClickPlayButton);
        }

        private void InitToggles()
        {
            var positionsCount = levelTogglesPositions.Length;
            
            _spawnedToggles = new LevelMapToggle[positionsCount];
            for (int i = 0; i < positionsCount; i++)
            {
                var position = levelTogglesPositions[i];
                var toggle = Instantiate(levelMapTogglePrefab, container);
                var index = i;
                toggle.SetPosition(position.anchoredPosition).Init(index, toggleGroup);

                _spawnedToggles[i] = toggle;
            }
        }

        private void OnSelectLevelToggle(int selectedLevel)
        {
            _selectedLevel = selectedLevel;
        }

        private void OnClickPlayButton()
        {
            
        }
    }
}