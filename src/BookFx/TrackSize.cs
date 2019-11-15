namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Track size is a column or row size.
    /// </summary>
    [PublicAPI]
    public struct TrackSize
    {
        public static readonly TrackSize None = new TrackSize(Mode.None);

        /// <summary>
        /// Auto fit using EPPlus AutoFit() feature. Wrapped and merged cells are ignored.
        /// </summary>
        public static readonly TrackSize Fit = new TrackSize(Mode.Fit);

        private readonly Mode _mode;
        private readonly float _value;

        private TrackSize(Mode mode)
        {
            _mode = mode;
            _value = default;
        }

        private TrackSize(float value)
        {
            _mode = Mode.Some;
            _value = value;
        }

        private enum Mode
        {
            None,
            Some,
            Fit,
        }

        public bool IsNone => _mode == Mode.None;

        /// <summary>
        /// Implicit convert from <see cref="float"/> to <see cref="TrackSize"/>.
        /// </summary>
        [Pure]
        public static implicit operator TrackSize(float value) => Some(value);

        [Pure]
        public static TrackSize Some(float value) => new TrackSize(value);

        public T Match<T>(Func<T> none, Func<T> fit, Func<float, T> some) =>
            _mode == Mode.None
                ? none()
                : _mode == Mode.Some
                    ? some(_value)
                    : fit();

        public IEnumerable<float> ValueAsEnumerable()
        {
            if (_mode == Mode.Some)
            {
                yield return _value;
            }
        }
    }
}